using Lab10Authentication.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services
{
    /// <summary>
    /// this intercace defines some methods for reading/assigning roles/users
    /// </summary>
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// this method signature returns an IQueryable of Roles from the database
        /// </summary>
        /// <returns></returns>
        IQueryable<IdentityRole> ReadAllRoles();

        /// <summary>
        /// this method signature returns an IQueryable of Users from the database
        /// </summary>
        /// <returns></returns>
        IQueryable<ApplicationUser> ReadAllUsers();

        /// <summary>
        /// this method signature takes in a username and rolename and assigns a role returning a boolean
        /// </summary>
        /// <param name="username"></param>
        /// <param name="rolename"></param>
        /// <returns></returns>
        bool AssignRole(string username, string rolename);
    }
}
