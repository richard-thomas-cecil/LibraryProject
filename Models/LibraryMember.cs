using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryProject.Models
{
    public class LibraryMember
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public double LateFees { get; set; }
        [NotMapped]
        public bool BookOverdue { get; set; }

    }
}
