using LibraryProject.DataAccess.Data;
using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryProject.DataAccess.Repository
{
    class LibraryMemberRepository : Repository<LibraryMember>, ILibraryMemberRepository
    {
        private readonly ApplicationDbContext _db;

        public LibraryMemberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(LibraryMember member)
        {
            var objFromDb = _db.LibraryMembers.FirstOrDefault(s => s.id == member.id);
            if(objFromDb != null)
            {
                objFromDb.Name = member.Name;
                objFromDb.PhoneNumber = member.PhoneNumber;
                objFromDb.StreetAddress = member.StreetAddress;
                objFromDb.State = member.State;
                objFromDb.City = member.City;
                objFromDb.PostalCode = member.PostalCode;
            }
        }

        public void UpdateLateFee(LibraryMember member)
        {
            var objFromDb = _db.LibraryMembers.FirstOrDefault(s => s.id == member.id);
            if(objFromDb != null)
            {
                objFromDb.LateFees = objFromDb.LateFees + member.LateFees;
            }
        }
    }
}
