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
    /// <summary>
    /// this class provides the main shopping view to the user as well as taking care of
    /// some authorization and redirection aspects of the web application
    /// </summary>
    [Authorize(Roles = "Shopper")]
    public class HomeController : Controller
    {
        private IItemRepository _itemRepo;
        private IProfileRepository _profileRepo;
        private UserManager<ApplicationUser> _userManager;
        private IUserRepository _userRepository;
        private IShoppingCartRepository _shoppingCartRepository;
        private ICartItemRepository _cartItemRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="itemRepo"></param>
        /// <param name="profileRepo"></param>
        /// <param name="userManager"></param>
        /// <param name="userRepository"></param>
        public HomeController(IItemRepository itemRepo, IProfileRepository profileRepo, UserManager<ApplicationUser> userManager, IUserRepository userRepository, IShoppingCartRepository shoppingCartRepository, ICartItemRepository cartItemRepository)
        {
            _itemRepo = itemRepo;
            _profileRepo = profileRepo;
            _userManager = userManager;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _cartItemRepository = cartItemRepository;
        }

        /// <summary>
        /// this is the main shopping view displayed to the shopper, which displays
        /// a list with every item that is available for purchase along with buy item and 
        /// view shopping cart links
        /// </summary>
        /// <returns></returns>
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

                var applicationUserId = _userManager.GetUserId(HttpContext.User);
                var shoppingCart = _shoppingCartRepository.ReadShoppingCart(applicationUserId);
                var shoppingCartList = _cartItemRepository.ReadAllCartItems().Where(c => c.CartId == shoppingCart.Id);

                decimal grandTotal = 0;
                int numItems = 0;

                foreach (var item in shoppingCartList)
                {
                    grandTotal += item.Price * item.Quantity;
                    numItems += item.Quantity;
                }

                ViewBag.grandTotal = grandTotal;
                ViewBag.numItems = numItems;

                return View(_itemRepo.ReadAllItems());
            }
            return View(_itemRepo.ReadAllItems());
        }

        /// <summary>
        /// this method displays a view with the web developer's information
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Sean Robinson";

            return View();
        }

        /// <summary>
        /// this method provides error handling functionality
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
