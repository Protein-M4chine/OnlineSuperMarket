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

namespace OnlineSupermarket.Controllers
{
    /// <summary>
    /// this controller provides methods that form the interface for
    /// the shopping cart (shoppers only)
    /// </summary>
    [Authorize(Roles="Shopper")]
    public class ShoppingCartController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IItemRepository _itemRepository;
        private ICartItemRepository _cartItemRepository;
        private IShoppingCartRepository _shoppingCartRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="usermanager"></param>
        /// <param name="itemRepository"></param>
        /// <param name="cartItemRepository"></param>
        /// <param name="shoppingCartRepository"></param>
        public ShoppingCartController(UserManager<ApplicationUser> usermanager,
                                      IItemRepository itemRepository,
                                      ICartItemRepository cartItemRepository,
                                      IShoppingCartRepository shoppingCartRepository)
        {
            _userManager = usermanager;
            _itemRepository = itemRepository;
            _cartItemRepository = cartItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        /// <summary>
        /// this is the method forms the Main Shopping view which displays
        /// all available items to buy grouped by type
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var applicationUserId = _userManager.GetUserId(HttpContext.User);
            var shoppingCart = _shoppingCartRepository.ReadShoppingCart(applicationUserId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { ApplicationUserId = applicationUserId };
            }

            var cartItemList = _cartItemRepository.ReadAllCartItems().Where(c => c.CartId == shoppingCart.Id);

           return View(cartItemList);
        }

        /// <summary>
        /// this is the get method which displays the buy view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Buy(int id, bool? error)
        {
            //find and get the item designated by the id parameter passed in to the action
            var item = _itemRepository.ReadItem(id);
            //get application user id of currently logged on user
            var applicationUserId = _userManager.GetUserId(HttpContext.User);
            var shoppingCart = _shoppingCartRepository.ReadShoppingCart(applicationUserId);

            if(shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
                shoppingCart.ApplicationUserId = applicationUserId;
                _shoppingCartRepository.CreateShoppingCart(shoppingCart);
            }
            return View(item);
        }

        /// <summary>
        /// this is the buy post method which processes the transaction
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Buy"), ValidateAntiForgeryToken]
        public IActionResult BuyConfirmed([Bind("id", "quantity")]int id, int quantity)
        {
            var item = _itemRepository.ReadItem(id);

            if(quantity > item.AmountInStock)
            {
                return RedirectToAction("Buy", "ShoppingCart",  new { id = item.Id });
            }

            var applicationUserId = _userManager.GetUserId(HttpContext.User);
            var shoppingCart = _shoppingCartRepository.ReadShoppingCart(applicationUserId);
            var cartItem = _cartItemRepository.ReadAllCartItems().FirstOrDefault(c => c.Name == item.Name && c.Type == item.Type && c.Price == item.Price);

            if (cartItem == null)
            {
                cartItem = new CartItem();
                cartItem.Name = item.Name;
                cartItem.Type = item.Type;
                cartItem.Price = item.Price;
                cartItem.Quantity = quantity;
                cartItem.CartId = shoppingCart.Id;
                cartItem.ShoppingCart = shoppingCart;
                _cartItemRepository.CreateCartItem(cartItem);
            }

            
            else
            {
                cartItem.Quantity += quantity;
            }

            item.AmountInStock -= quantity;
            _itemRepository.UpdateItem(item.Id, item);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// this is the remove item get method which displays the confirmation
        /// page to the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Remove(int id)
        {
            var cartItem = _cartItemRepository.ReadCartItem(id);

            return View(cartItem);
        }

        /// <summary>
        /// this is the remove item post method which process the transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Remove"),ValidateAntiForgeryToken]
        public IActionResult RemoveConfirmed(int id)
        {
            var cartItem = _cartItemRepository.ReadCartItem(id);

            var item = _itemRepository.ReadAllItems().FirstOrDefault(c => c.Name == cartItem.Name && c.Type == cartItem.Type && c.Price == cartItem.Price);

            item.AmountInStock += cartItem.Quantity;

            _itemRepository.UpdateItem(item.Id, item);

            _cartItemRepository.DeleteCartItem(id);

            return RedirectToAction("Index", "ShoppingCart");
        }

        /// <summary>
        /// this is the get method to empty the shopping cart which displays the
        /// confirmation view to the user
        /// </summary>
        /// <returns></returns>
        public IActionResult Empty()
        {
            return View();
        }

        /// <summary>
        /// this is the empty cart post method which processes the transaction
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("Empty"), ValidateAntiForgeryToken]
        public IActionResult EmptyConfirmed()
        {
            var cartList = _cartItemRepository.ReadAllCartItems();

            foreach (var cartItem in cartList)
            {
                var item = _itemRepository.ReadAllItems().FirstOrDefault(c => c.Name == cartItem.Name && c.Type == cartItem.Type && c.Price == cartItem.Price);

                item.AmountInStock += cartItem.Quantity;

                _itemRepository.UpdateItem(item.Id, item);

                _cartItemRepository.DeleteCartItem(cartItem.Id);
            }
            return RedirectToAction("Index", "ShoppingCart");
        }


        /// <summary>
        /// this is the get method for cart checkout which displays the confirmation
        /// view to the user
        /// </summary>
        /// <returns></returns>
        public IActionResult Checkout()
        {
            var cartItemList = _cartItemRepository.ReadAllCartItems();

            return View(cartItemList);
        }

        /// <summary>
        /// this is the post method for checkout which processes the transaction
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("Checkout"), ValidateAntiForgeryToken]
        public IActionResult CheckoutConfirmed()
        {
            var cartList = _cartItemRepository.ReadAllCartItems();

            foreach (var cartItem in cartList)
            {
                _cartItemRepository.DeleteCartItem(cartItem.Id);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}