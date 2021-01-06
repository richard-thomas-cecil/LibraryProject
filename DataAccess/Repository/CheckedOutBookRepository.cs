using LibraryProject.DataAccess.Data;
using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository
{
    public class CheckedOutBookRepository : Repository<CheckedOutBook>, ICheckedOutBookRepository
    {
        private readonly ApplicationDbContext _db;

        public CheckedOutBookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CheckedOutBook checkedOutBook)
        {
            _db.Update(checkedOutBook);
        }
    }
}
