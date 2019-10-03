using Lab10Authentication.Data;
using Lab10Authentication.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services
{
    /// <summary>
    /// this class initializes users and roles and is called upon program startup
    /// </summary>
    public class Initializer
    {
        /// <summary>
        /// inject necessary repositories
        /// </summary>
        private ApplicationDbContext _context;
        private RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        public Initializer(
           ApplicationDbContext context,
           RoleManager<IdentityRole> roleManager,
           UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// this initiates the different roles and creates the admin user setting its information
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

            if (!_context.Roles.Any(r => r.Name == "Shopper"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Shopper" });
            }


            if (!_context.Users.Any(u => u.UserName == "admin@user.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "admin@user.com",
                    UserName = "admin@user.com"
                };
                await _userManager.CreateAsync(user, "Pass123!");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }

    }
}
