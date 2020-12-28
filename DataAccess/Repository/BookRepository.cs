using LibraryProject.DataAccess.Data;
using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Book book)
        {
            var objFromDb = _db.Books.FirstOrDefault(s => s.id == book.id);
            if (objFromDb != null)
            {
                objFromDb.Title = book.Title;
                objFromDb.AuthorFirstName = book.AuthorFirstName;
                objFromDb.AuthorLastName = book.AuthorLastName;
                objFromDb.Description = book.Description;
                objFromDb.ISBN = book.ISBN;
                objFromDb.GenreId = book.GenreId;
                objFromDb.ImageUrl = book.ImageUrl;
                objFromDb.Total = book.Total;
                objFromDb.CheckedOut = book.CheckedOut;
            }
        }
    }
    
}
