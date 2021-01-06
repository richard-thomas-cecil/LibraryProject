using LibraryProject.DataAccess.Data;
using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository
{
    class MemberUserRepository : Repository<MemberUser>, IMemberUserRepository
    {
        private readonly ApplicationDbContext _db;

        public MemberUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
