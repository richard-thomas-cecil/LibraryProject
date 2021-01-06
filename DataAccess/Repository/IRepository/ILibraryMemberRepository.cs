using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository.IRepository
{
    public interface ILibraryMemberRepository : IRepository<LibraryMember>
    {
        void Update(LibraryMember member);
        void UpdateLateFee(LibraryMember member);
    }
}
