using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10Authentication.Controllers
{
    /// <summary>
    /// this class contains a method for reading available roles within the database
    /// (provided by Authentication Lab)
    /// </summary>
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private IAuthenticationRepository _repo;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        public RolesController(IAuthenticationRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// this method displays a view to the admin which contains all the 
        /// available roles within the database
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_repo.ReadAllRoles());
        }
    }
}