using LibraryProject.DataAccess.Data;
using LibraryProject.DataAccess.Repository.IRepository;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LibraryProject.DataAccess.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Genre genre)
        {
            var objFromDb = _db.Genres.FirstOrDefault(s => s.id == genre.id);
            if(objFromDb != null)
            {
                objFromDb.name = genre.name;
            }
        }
    }
}
