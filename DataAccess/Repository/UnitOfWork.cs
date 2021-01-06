using LibraryProject.DataAccess.Data;
using LibraryProject.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Genre = new GenreRepository(_db);
            Book = new BookRepository(_db);
            LibraryMember = new LibraryMemberRepository(_db);
            EmployeeUser = new EmployeeUserRepository(_db);
            MemberUser = new MemberUserRepository(_db);
            CheckedOutBook = new CheckedOutBookRepository(_db);
        }

        public IGenreRepository Genre { get; private set; }
        public IBookRepository Book { get; private set; }
        public ILibraryMemberRepository LibraryMember { get; private set; }
        public IEmployeeUserRepository EmployeeUser { get; private set; }
        public IMemberUserRepository MemberUser { get; private set; }
        public ICheckedOutBookRepository CheckedOutBook { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
