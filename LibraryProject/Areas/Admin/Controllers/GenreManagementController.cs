using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreManagementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Genre genre = new Genre();

            if (id == null)
            {
                return View(genre);
            }

            genre = _unitOfWork.Genre.Get(id.GetValueOrDefault());
            if(genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (genre.id == 0)
                {
                    _unitOfWork.Genre.Add(genre);
                    _unitOfWork.Save();
                }

                else
                {
                    _unitOfWork.Genre.Update(genre);
                    _unitOfWork.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Genre.GetAll();
            return Json(new { data = allObj });
        }

        #endregion

    }
}
