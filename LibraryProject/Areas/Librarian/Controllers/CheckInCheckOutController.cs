using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace LibraryProject.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Roles = SD.Role_Admin_Employee + "," + SD.Role_Librarian_Employee + "," + SD.Role_Assistant_Employee)]
    public class CheckInCheckOutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private double overdueFee;

        public CheckInCheckOutController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            overdueFee = Double.Parse(configuration["OverdueFee"]);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var bookObj = _unitOfWork.Book.GetFirstOrDefault(b => b.id == id, includeProperties:"genre");

            return View(bookObj);
        }

        public IActionResult CheckInOut()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckInOut(CheckedOutBook checkedOutBook, string checkOut, string checkIn)
        {
            checkedOutBook.BookId = _unitOfWork.Book.GetFirstOrDefault(u => u.ISBN.Equals(checkedOutBook.Book.ISBN)).id;
            Book book = _unitOfWork.Book.Get(checkedOutBook.BookId);
            checkedOutBook.Member = _unitOfWork.LibraryMember.Get(checkedOutBook.MemberId);

            checkedOutBook.Member.BookOverdue = HasOverdueBooks(checkedOutBook.MemberId);

            book.Available = book.Total - book.CheckedOut;

            
            if(book == null)
            {
                ModelState.AddModelError("", "ISBN Not Valid");
                return View(checkedOutBook);
            }
            if ( _unitOfWork.LibraryMember.Get(checkedOutBook.MemberId) == null)
            {
                ModelState.AddModelError("", "Member ID Not Valid");
            }

            if (!string.IsNullOrEmpty(checkOut))
            {
                //Member cannot checkout books if they have overdue books or unpaid late fees
                if (checkedOutBook.Member.BookOverdue)
                {
                    ModelState.AddModelError("", "Member has overdue books");
                    return View(checkedOutBook);
                }
                if(checkedOutBook.Member.LateFees > 0)
                {
                    ModelState.AddModelError("", "Memeber has unpaid late fees");
                }

                if (book.Available <= 0)
                {
                    ModelState.AddModelError("", "All available copies of " + book.Title + " are checked out");
                    return View();
                }
                //In this implementation, user is only allowed to checkout one copy of a book
                if (_unitOfWork.CheckedOutBook.GetFirstOrDefault(s => s.MemberId == checkedOutBook.MemberId) != null)
                {
                    ModelState.AddModelError("", _unitOfWork.LibraryMember.Get(checkedOutBook.MemberId).Name + " has already checked out " + book.Title);
                    return View();
                }

                checkedOutBook.Book = _unitOfWork.Book.Get(checkedOutBook.BookId);
                checkedOutBook.DueDate = DateTime.Now.AddDays(14);

                book.CheckedOut++;
                _unitOfWork.Book.UpdateCheckOut(book);
                _unitOfWork.CheckedOutBook.Add(checkedOutBook);
                _unitOfWork.Save();
                return RedirectToAction(nameof(ConfirmCheckInOut), checkedOutBook);
            }
            if (!string.IsNullOrEmpty(checkIn))
            {
                checkedOutBook = _unitOfWork.CheckedOutBook.GetFirstOrDefault(s=> s.MemberId == checkedOutBook.MemberId);

                //Update users late fee if book is overdue
                if (DateTime.Compare(checkedOutBook.DueDate, DateTime.Now) < 0)
                {
                    checkedOutBook.Member.LateFees = DateTime.Now.Subtract(checkedOutBook.DueDate).Days * overdueFee;
                    _unitOfWork.LibraryMember.UpdateLateFee(checkedOutBook.Member);
                }

                if (book.CheckedOut <= 0)
                {
                    ModelState.AddModelError("", "No copies of " + book.Title + " are checked out");
                    return View();
                }
                if(checkedOutBook == null)
                {
                    ModelState.AddModelError("", "Selected Member has not checked out this book");
                    return View(checkedOutBook);
                }
                
                book.CheckedOut--;
                _unitOfWork.Book.UpdateCheckOut(book);
                _unitOfWork.CheckedOutBook.Remove(checkedOutBook);
                _unitOfWork.Save();
                return RedirectToAction(nameof(ConfirmCheckInOut), checkedOutBook);
            }
            ModelState.AddModelError("", "Unknown Error Occurred");
            return View(checkedOutBook);
        }




        public IActionResult ConfirmCheckInOut(CheckedOutBook checkedOutBook)
        {
            
            return View(checkedOutBook);
        }

        //Methods for original implementation of Check In and Check Out. No longer used in this controller, but were reimplemented in the BookView Controller.
        //Keeping here for posterity
        #region DEPRECATED
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

            if(_unitOfWork.LibraryMember.Get(checkedOutBook.MemberId) == null)
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

            if(checkedOutBook == null)
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
        #endregion

        public bool HasOverdueBooks(int memberId)
        {
            var memberCheckedOutBooks = _unitOfWork.CheckedOutBook.GetAll(u => u.MemberId == memberId);
            bool overdue = false;
            foreach(var book in memberCheckedOutBooks)
            {
                if(DateTime.Compare(book.DueDate, DateTime.Now) < 0)
                {
                    overdue = true;
                }
            }
            return overdue;
        }
        

        #region API CALLS
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Book.GetAll(includeProperties:"genre");
            foreach(var book in allObj)
            {
                book.Available = book.Total - book.CheckedOut;
            }
            return Json(new { data = allObj });
        }

        //Check In method for checking in from Library Members page, used in LibraryMemberDetails
        [HttpPost]
        public IActionResult CheckInPost([FromBody] string[] bookMemberId)
        {
            //bookMemberId is passed as string array from LibraryMemberDetails.js with bookId in index [0] and memberId in index [1]. This is then used to pull the correct checkedout book from the CheckedOutBook table
            CheckedOutBook checkedOutBook = _unitOfWork.CheckedOutBook.GetFirstOrDefault(u => (u.BookId == Int32.Parse(bookMemberId[0])) && (u.MemberId == Int32.Parse(bookMemberId[1])), includeProperties: "Book,Member");

            if(checkedOutBook == null)
            {
                return Json(new { success = false });
            }

            if (DateTime.Compare(checkedOutBook.DueDate, DateTime.Now) < 0)
            {
                checkedOutBook.Member.LateFees = DateTime.Now.Subtract(checkedOutBook.DueDate).Days * overdueFee;
                _unitOfWork.LibraryMember.UpdateLateFee(checkedOutBook.Member);
            }

            checkedOutBook.Book.CheckedOut--;
            _unitOfWork.Book.UpdateCheckOut(checkedOutBook.Book);
            _unitOfWork.CheckedOutBook.Remove(checkedOutBook);
            _unitOfWork.Save();

            return Json(new { success = true});
        }
        #endregion
    }
}
