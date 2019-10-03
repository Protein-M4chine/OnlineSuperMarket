using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineSupermarket.Controllers
{
    public class ShoppingCartController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}