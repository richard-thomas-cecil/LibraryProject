using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using LibraryProject.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Areas.User.Controllers
{
    [Area("User")]
    public class BookViewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookViewController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var bookObj = _unitOfWork.Book.GetFirstOrDefault(b => b.id == id, includeProperties: "genre");

            return View(bookObj);
        }

        public IActionResult CheckOut(int id)
        {
            CheckedOutBook checkedOutBook = new CheckedOutBook
            {
                BookId = id,
            };

            return View(checkedOutBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut(CheckedOutBook checkedOutBook)
        {
            checkedOutBook.id = 0;
            Book book = _unitOfWork.Book.Get(checkedOutBook.BookId);
            book.Available = book.Total - book.CheckedOut;

            checkedOutBook.DueDate = DateTime.Now.AddDays(14);

            if (_unitOfWork.LibraryMember.Get(checkedOutBook.MemberId) == null)
            {
                ModelState.AddModelError("", "Member Does Not Exist");
                return View(checkedOutBook);
            }

            if (book.Available > 0)
            {
                book.CheckedOut++;
                _unitOfWork.Book.UpdateCheckOut(book);
                _unitOfWork.CheckedOutBook.Add(checkedOutBook);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Unkown Error Occurred");
            return View(checkedOutBook);


        }

        public IActionResult CheckIn(int id)
        {
            CheckedOutBook checkedOutBook = new CheckedOutBook
            {
                BookId = id,
            };

            return View(checkedOutBook);
        }

        [HttpPost]
        public IActionResult CheckIn(CheckedOutBook checkedOutBook)
        {
            Book book = _unitOfWork.Book.Get(checkedOutBook.BookId);
            checkedOutBook = _unitOfWork.CheckedOutBook.GetFirstOrDefault(u => u.MemberId == checkedOutBook.MemberId);

            if (checkedOutBook == null)
            {
                ModelState.AddModelError("", "Selected Member has not checked out this book");
                return View(checkedOutBook);
            }

            if (book.CheckedOut > 0)
            {
                book.CheckedOut--;
                _unitOfWork.Book.UpdateCheckOut(book);
                _unitOfWork.CheckedOutBook.Remove(checkedOutBook);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Unkown Error Occurred");
            return View(book);
        }
        #region API CALLS
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Book.GetAll(includeProperties: "genre");
            foreach (var book in allObj)
            {
                book.Available = book.Total - book.CheckedOut;
            }
            return Json(new { data = allObj });
        }
        #endregion
    }
}
