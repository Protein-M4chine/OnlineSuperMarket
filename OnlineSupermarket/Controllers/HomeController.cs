using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab10Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Lab10Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Lab10Authentication.Controllers
{
    [Authorize(Roles = "Shopper")]
    public class HomeController : Controller
    {
        private IItemRepository _itemRepo;
        private IProfileRepository _profileRepo;
        private UserManager<ApplicationUser> _userManager;
        private IUserRepository _userRepository;

        public HomeController(IItemRepository itemRepo, IProfileRepository profileRepo, UserManager<ApplicationUser> userManager,
                              IUserRepository userRepository)
        {
            _itemRepo = itemRepo;
            _profileRepo = profileRepo;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Item");
            }

            if (User.IsInRole("Shopper"))
            {
                if (_userRepository.ReadUser(User.Identity.Name).Profile == null)
                {
                    return RedirectToAction("Create", "Profile");
                }
                return View(_itemRepo.ReadAllItems());
                //return View(_user.ReadUser(User.Identity.Name).Profile);
            }

            return View(_itemRepo.ReadAllItems());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Sean Robinson";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
