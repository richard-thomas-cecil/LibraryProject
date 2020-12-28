using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book book);
    }
}
