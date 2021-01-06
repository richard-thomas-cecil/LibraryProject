using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryProject.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }

        public string Name { get; set; }

    }
}
