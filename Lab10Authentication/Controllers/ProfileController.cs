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
    /// <summary>
    /// this class provides methods for interacting with user profiles (shoppers only)
    /// </summary>
    [Authorize(Roles="Shopper")]
    public class ProfileController : Controller
    {
        private IProfileRepository _profileRepo;
        private UserManager<ApplicationUser> _userManager;
        private IUserRepository _userRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="profileRepo"></param>
        /// <param name="userManager"></param>
        /// <param name="userRepository"></param>
        public ProfileController(IProfileRepository profileRepo, UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            _profileRepo = profileRepo;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        /// <summary>
        /// this method displays a form that allows a user to create a profile
        /// </summary>
        /// <returns></returns>
        public IActionResult Create(bool? error)
        {
            var profile = _userRepository.ReadUser(User.Identity.Name).Profile;
            if (profile != null)
            {
                return RedirectToAction("Index", "Home");
            }
 
            ViewBag.errorMessage = "";

            if (error == true)
                ViewBag.errorMessage = "This username is already taken";

            return View();
        }

        /// <summary>
        /// this is the create profile post method that processes the form
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Email, UserName, FirstName, LastName, Street, State, Zip")]Profile profile)
        {
            var profileList = _profileRepo.ReadAllProfiles();


            if (ModelState.IsValid)
            {
                foreach (var profileItem in profileList)
                {
                    if(profileItem.UserName == profile.UserName)
                    {
                        return RedirectToAction("Create", "Profile", new { error = true});
                    }
                }
                _userRepository.ReadUser(User.Identity.Name).Profile = profile;
                _profileRepo.CreateProfile(profile);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /// <summary>
        /// this method displays the details of user's profile in it's own view
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IActionResult Details(string email)
        {
            var profile = _profileRepo.ReadProfile(email);
            if (profile == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(profile);
        }

        /// <summary>
        /// this is the edit profile get method which displays a form which
        /// allows a user to edit the profile properties
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public IActionResult Edit(string Email)
        {
            var profile = _profileRepo.ReadProfile(Email);
            if (profile == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View(profile);
        }

        /// <summary>
        /// this is the post edit profile method which processes the changes made
        /// by the user to his/her profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Email, UserName, FirstName, LastName, Street, State, Zip")]Profile profile)
        {
            var OldProfile = _profileRepo.ReadProfile(profile.Email);
            if (ModelState.IsValid)
            {
                OldProfile.FirstName = profile.FirstName;
                OldProfile.LastName = profile.LastName;
                OldProfile.Street = profile.Street;
                OldProfile.State = profile.State;
                OldProfile.Zip = profile.Zip;
            }
            _profileRepo.UpdateProfile(OldProfile);
            return RedirectToAction("Details", "Profile", new { email = profile.Email});
        }

        /// <summary>
        /// this is the delete profile get method which displays confirmation to 
        /// the user asking him/her if they are sure they wish to delete their profile
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IActionResult Delete(string email)
        {
            var profile = _profileRepo.ReadProfile(email);
            if (profile == null)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View(profile);
        }

        /// <summary>
        /// this is post profile delete method which processes the user request
        /// to delete his/her profile
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([Bind("Email, UserName, FirstName, LastName, Street, State, Zip")]string email)
        {
            _profileRepo.DeleteProfile(email);
            return RedirectToAction("Index", "Home");
        }


    }
}