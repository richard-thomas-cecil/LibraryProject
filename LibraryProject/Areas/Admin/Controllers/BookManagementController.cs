using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using LibraryProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace LibraryProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=SD.Role_Admin_Employee + "," + SD.Role_Librarian_Employee)]
     
    public class BookManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookManagementController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert (int? id)
        {
            BookViewModel bookVM = new BookViewModel()
            {
                book = new Book(),
                GenreList = _unitOfWork.Genre.GetAll().Select(i => new SelectListItem
                {
                    Text = i.name,
                    Value = i.id.ToString()
                })

            };
            if(id == null)
            {
                return View(bookVM);
            }
            bookVM.book = _unitOfWork.Book.Get(id.GetValueOrDefault());
            if (bookVM.book == null)
            {
                return NotFound();
            }
            return View(bookVM);
        }

        [HttpPost]
        public IActionResult Upsert(BookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\BookCovers");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (bookVM.book.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, bookVM.book.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filestreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestreams);
                    }

                    bookVM.book.ImageUrl = @"\Images\BookCovers\" + fileName + extension;
                }
                else
                {
                    if (bookVM.book.id != 0)
                    {
                        Book objFromDb = _unitOfWork.Book.Get(bookVM.book.id);
                        bookVM.book.ImageUrl = objFromDb.ImageUrl;
                    }
                }
                if (bookVM.book.id == 0)
                {
                    bookVM.book.DateAdded = DateTime.Today;
                    _unitOfWork.Book.Add(bookVM.book);
                }
                else
                {
                    _unitOfWork.Book.Update(bookVM.book);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            else
            {
                bookVM.GenreList = _unitOfWork.Genre.GetAll().Select(i => new SelectListItem
                {
                    Text = i.name,
                    Value = i.id.ToString()
                });
                if (bookVM.book.id != 0)
                {
                    bookVM.book = _unitOfWork.Book.Get(bookVM.book.id);
                }
            }
            return View(bookVM);
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Book.GetAll(includeProperties:"genre");
            return Json(new { data =allObj });
        }
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Book.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting Book" });
            }

            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _unitOfWork.Book.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Book Deleted" });
        }
        #endregion
    }
}
