using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10Authentication.Models.ViewModels;
using Lab10Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10Authentication.Controllers
{
    /// <summary>
    /// this class provides functinality for viewing users and assinging roles
    /// (provided by Authentication Lab)
    /// </summary>
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private IAuthenticationRepository _repo;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        public UserController(IAuthenticationRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// this method displays a view with a list of all users in the database
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var users = _repo.ReadAllUsers();
            var userList = users
               .Select(u => new UserListViewModel
               {
                   Email = u.Email,
                   UserName = u.UserName,
                   NumberOfRoles = u.RoleNames.Count
               });
            return View(userList);
        }

        /// <summary>
        /// this is a assign role get method which displays a view which allows an admin
        /// to assign roles to users
        /// </summary>
        /// <returns></returns>
        public IActionResult AssignRole()
        {
            var users = _repo.ReadAllUsers();
            var roles = _repo.ReadAllRoles();
            var model = new AssignRoleViewModel
            {

            };
            foreach (var user in users)
            {
                model.UserNames.Add(user.UserName);
            }
            foreach (var role in roles)
            {
                model.RoleNames.Add(role.Name);
            }

            return View(model);
        }

        /// <summary>
        /// this is the assign role post method which processes the changes to roles and
        /// and users that the admin makes
        /// </summary>
        /// <param name="username"></param>
        /// <param name="rolename"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AssignRole(string username, string rolename)
        {
            _repo.AssignRole(username, rolename);
            return RedirectToAction("Index");
        }
    }
}