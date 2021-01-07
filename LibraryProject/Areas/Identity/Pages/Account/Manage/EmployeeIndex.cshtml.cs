using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryProject.Areas.Identity.Pages.Account.Manage
{
    public partial class EmployeeIndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeIndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public string Username { get; set; }
        public EmployeeUser Employee { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //Input Model not necessary for employees
        //[BindProperty]
        //public InputModel Input { get; set; }

        //public class InputModel
        //{
        //    public EmployeeUser employee { get; set; }
        //}

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            Username = userName;

            //Take the default Identity user and get the MemberUser implementation information for use in the model
            Employee = _unitOfWork.EmployeeUser.GetFirstOrDefault(u => u.UserName == userName);

           
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }


        //Employees can not modify any information, so post is not necessary
//        public async Task<IActionResult> OnPostAsync()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            if (!ModelState.IsValid)
//            {
//                await LoadAsync(user);
//                return Page();
//            }
//;

//            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
//            //if (Input.member.PhoneNumber != phoneNumber)
//            //{
//            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.member.PhoneNumber);
//            //    if (!setPhoneResult.Succeeded)
//            //    {
//            //        StatusMessage = "Unexpected error when trying to set phone number.";
//            //        return RedirectToPage();
//            //    }
//            //}

//            await _signInManager.RefreshSignInAsync(user);
//            StatusMessage = "Your profile has been updated";
//            return RedirectToPage();
//        }
    }
}
