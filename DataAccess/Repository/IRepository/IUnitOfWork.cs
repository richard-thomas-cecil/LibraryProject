using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository Genre { get;  }
        IBookRepository Book { get; }
        IEmployeeUserRepository EmployeeUser { get; }
        ILibraryMemberRepository LibraryMember { get; }
        IMemberUserRepository MemberUser { get;  }
        ICheckedOutBookRepository CheckedOutBook { get; }

        void Save();
    }
}
