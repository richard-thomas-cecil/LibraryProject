using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository Genre { get; set; }

        void Save();
    }
}
