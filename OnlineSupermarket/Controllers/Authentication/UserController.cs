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
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private IAuthenticationRepository _repo;

        public UserController(IAuthenticationRepository repo)
        {
            _repo = repo;
        }
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


        [HttpPost]
        public IActionResult AssignRole(string username, string rolename)
        {
            _repo.AssignRole(username, rolename);
            return RedirectToAction("Index");
        }
    }
}