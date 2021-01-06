using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryProject.Areas.Identity.Pages.Account
{
    public class MemberIdModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public string ReturnUrl { get; set; }

        public MemberIdModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public int MemberId { get; set; }
        }

        public void OnGet(string returnUrl=null)
        {
            ReturnUrl = returnUrl;
        }

       

        public IActionResult OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                 var member = _unitOfWork.LibraryMember.Get(Input.MemberId);
                if(member == null)
                {
                    ModelState.AddModelError(string.Empty, "Member ID not Valid");
                    return Page();
                }
                if(_unitOfWork.MemberUser.GetFirstOrDefault(u=> u.MemberId == Input.MemberId) != null)
                {
                    ModelState.AddModelError(string.Empty, "Account already exsists for this ID");
                    return Page();
                }

                return RedirectToPage("Register", member);
            }

            return Page();
        }
    }
}
