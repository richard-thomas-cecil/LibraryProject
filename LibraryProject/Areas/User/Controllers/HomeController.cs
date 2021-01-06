using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using LibraryProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetLatestBooks()
        {
            var allObj = _unitOfWork.Book.GetAll(includeProperties:"genre").ToList();
            List<Book> latestAdded = new List<Book>();

            for(int i = (allObj.Count() - 1); i > allObj.Count() - 6 && i >= 0; i--)
            {
                latestAdded.Add(allObj[i]);
            }

            IEnumerable<Book> latest = latestAdded;

            return Json(new { data = latest });
        }
        #endregion
    }
}
