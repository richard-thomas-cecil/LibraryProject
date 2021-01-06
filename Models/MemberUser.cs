
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryProject.Models
{
    public class MemberUser : IdentityUser
    {
        [Required]
        public int MemberId { get; set; }
        //Library Members are not required to register an account as most library functions are performed on site. A Library Member may register and account for online functions if they choose to,
        //in which case they will set up an account using there ID generated at the library
        [ForeignKey("MemberId")]
        public LibraryMember Member { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
