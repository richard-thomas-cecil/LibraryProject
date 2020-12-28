using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryProject.Models.ViewModels
{
    public class BookViewModel
    {
        public Book book { get; set; }
        public IEnumerable<SelectListItem> GenreList { get; set; }
    }
}
