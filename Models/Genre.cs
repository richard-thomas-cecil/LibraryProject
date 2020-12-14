using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Models
{
    public class Genre
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Genre")]
        [Required]
        [MaxLength(20)]
        public string name { get; set; }
    }
}
