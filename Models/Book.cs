using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryProject.Models
{
    public class Book
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Title { get; set; }        
        public string AuthorFirstName { get; set; }
        [Required]
        public string AuthorLastName { get; set; }
        [Required]
        public string ISBN { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1,100)]
        public int Total { get; set; }
        public int CheckedOut { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre genre { get; set; }
    }
}
