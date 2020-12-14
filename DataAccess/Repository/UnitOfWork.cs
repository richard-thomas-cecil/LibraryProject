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
        }

        public IGenreRepository Genre { get; set; }

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
