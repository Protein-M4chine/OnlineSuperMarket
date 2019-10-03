using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10Authentication.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private IAuthenticationRepository _repo;

        public RolesController(IAuthenticationRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View(_repo.ReadAllRoles());
        }
    }
}