using LibraryProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<EmployeeUser> EmployeeUsers { get; set; }
        public DbSet<MemberUser> MemberUsers { get; set; }
        public DbSet<LibraryMember> LibraryMembers  { get; set; }
        public DbSet<CheckedOutBook> CheckedOutBooks { get; set; }
    }
}
