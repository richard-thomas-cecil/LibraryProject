using LibraryProject.DataAccess.Data;
using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository
{
    public class EmployeeUserRepository : Repository<EmployeeUser>, IEmployeeUserRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
