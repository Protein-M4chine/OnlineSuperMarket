using Lab10Authentication.Data;
using Lab10Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services
{
    /// <summary>
    /// this class implements the IAuthenticationRepository
    /// </summary>
    public class DbAuthenticationRepository : IAuthenticationRepository
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userManager"></param>
        public DbAuthenticationRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        /// <summary>
        /// this method returns an IQueryable of Roles from the database
        /// </summary>
        /// <returns></returns>
        public IQueryable<IdentityRole> ReadAllRoles()
        {
            return _db.Roles;
        }

        /// <summary>
        /// this method returns an IQueryable of Users from the database
        /// </summary>
        /// <returns></returns>
        public IQueryable<ApplicationUser> ReadAllUsers()
        {
            var users = _db.Users;
            // For each user, load the role names
            users.ForEachAsync(u => u.RoleNames = ReadRoleNames(u)).Wait();
            return users;
        }

        public bool AssignRole(string username, string rolename)
        {

            var user = ReadAllUsers().FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                var role = user.RoleNames.FirstOrDefault(rn => rn == rolename);
                // If the user does not have this role   
                if (role == null)
                {
                    _userManager.AddToRoleAsync(user, rolename).Wait();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// this method takes in a username and rolename and assigns a role returning a boolean
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private ICollection<string> ReadRoleNames(ApplicationUser user)
        {
            var roleNames = _userManager.GetRolesAsync(user);
            roleNames.Wait();
            return roleNames.Result;
        }
    }
}
