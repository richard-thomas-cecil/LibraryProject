using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace LibraryProject.Areas.Librarian.Controllers
{
    [Area("Librarian")]
    [Authorize(Roles = SD.Role_Admin_Employee + "," + SD.Role_Librarian_Employee + "," + SD.Role_Assistant_Employee)]
    public class LibraryMemberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LibraryMemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult Upsert(int? id)
        {
            LibraryMember member = new LibraryMember();
            if(id == null)
            {
                return View(member);
            }
            member = _unitOfWork.LibraryMember.Get(id.GetValueOrDefault());
            if(member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        public IActionResult Upsert(LibraryMember member)
        {
            if (ModelState.IsValid)
            {
                if(member.id == 0)
                {
                    member.JoinDate = DateTime.Now;
                    _unitOfWork.LibraryMember.Add(member);
                    
                }
                else
                {
                    _unitOfWork.LibraryMember.Update(member);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public IActionResult Details(int id)
        {
            LibraryMember member = _unitOfWork.LibraryMember.Get(id);
            return View(member);
        }

        public IActionResult PayLateFee(int id)
        {
            LibraryMember member = _unitOfWork.LibraryMember.Get(id);
            return View(member);
        }

        [HttpPost]
        public IActionResult PayLateFee(LibraryMember member, double PaidFee)
        {

            double change = 0;

            //If member pays more than required amount, give out change and invert LateFees. When passed to UpdateLateFee, this will subtract full total and set member.LateFee to 0 in Database
            if (member.LateFees - PaidFee < 0)
            {
                change = member.LateFees - PaidFee;
                member.LateFees = member.LateFees * -1;
            }
            //Set Late Fee to inversion of PaidFee. When passed to UpdateLateFee, this will subtract paid amount from member.LateFee and save to DB
            else {
                member.LateFees = PaidFee * -1;
            }

            _unitOfWork.LibraryMember.UpdateLateFee(member);
            _unitOfWork.Save();
            return RedirectToAction(nameof(ConfirmPayment), change);
            
        }

        public IActionResult ConfirmPayment(double change)
        {
            TempData["change"] = change;
            return View();
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.LibraryMember.GetAll();
            return Json(new { data = allObj });

        }
        [HttpGet]
        public IActionResult GetOverdue(string id)
        {
            var overdueBooks = _unitOfWork.CheckedOutBook.GetAll(u => u.MemberId == Int32.Parse(id), includeProperties: "Book");
            return Json(new { data = overdueBooks });
        }
        #endregion
    }


}
