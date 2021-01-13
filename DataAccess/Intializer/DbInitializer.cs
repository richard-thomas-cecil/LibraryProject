using LibraryProject.DataAccess.Data;
using LibraryProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace LibraryProject.DataAccess.Intializer
{
    public class DbInitializer : IDbIntializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Intialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }
            if (_db.Roles.Any(r => r.Name == SD.Role_Admin_Employee)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin_Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Librarian_Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Assistant_Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Member)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new EmployeeUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin",
                EmployeeNumber = 1
            });

            EmployeeUser user = _db.EmployeeUsers.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();

            _userManager.AddToRoleAsync(user, SD.Role_Admin_Employee).GetAwaiter().GetResult();
        }

        
    }
}
