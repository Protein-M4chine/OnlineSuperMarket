using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10Authentication.Models;
using Lab10Authentication.Models.Entities;
using Lab10Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab10Authentication.Controllers
{
    [Authorize(Roles="Shopper")]
    public class ProfileController : Controller
    {
        private IProfileRepository _profileRepo;
        private UserManager<ApplicationUser> _userManager;

        public ProfileController(IProfileRepository profileRepo, UserManager<ApplicationUser> userManager)
        {
            _profileRepo = profileRepo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_profileRepo.ReadAllProfiles());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Email, Username, FirstName, Last, Street, State, Zip")]Profile profile)
        {
            if (ModelState.IsValid)
            {
                _profileRepo.CreateProfile(profile);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Details(string email)
        {
            var profile = _profileRepo.ReadProfile(email);
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(profile);
        }

        public IActionResult Edit(string Email)
        {
            var profile = _profileRepo.ReadProfile(Email);
            if (profile == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View(profile);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Email, Username, FirstName, Last, Street, State, Zip")]Profile profile)
        {
            if (ModelState.IsValid)
            {
                _profileRepo.UpdateProfile(profile);
                return RedirectToAction("Details", "Profile", new { email = profile.Email});
            }
            return View(profile);
        }

        public IActionResult Delete(string email)
        {
            var profile = _profileRepo.ReadProfile(email);
            if (profile == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View(profile);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([Bind("Email, Username, FirstName, Last, Street, State, Zip")]string email)
        {
            _profileRepo.DeleteProfile(email);
            return RedirectToAction("Index", "Home");
        }


    }
}