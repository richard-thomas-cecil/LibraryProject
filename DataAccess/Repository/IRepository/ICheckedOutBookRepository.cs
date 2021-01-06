using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Repository.IRepository
{
    public interface ICheckedOutBookRepository : IRepository<CheckedOutBook>
    {
        void Update(CheckedOutBook checkedOutBook);
    }
}
