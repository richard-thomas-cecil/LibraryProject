using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryProject.Models
{
    public class CheckedOutBook
    {  
        [Key]
        public int id { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public LibraryMember Member { get; set; }

        public DateTime DueDate { get; set; }

        [NotMapped]
        public double LateFee { get; set; }
    }
}
