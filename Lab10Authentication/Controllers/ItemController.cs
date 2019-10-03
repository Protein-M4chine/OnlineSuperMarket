using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab10Authentication.Models.Entities;
using Lab10Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10Authentication.Controllers
{
    /// <summary>
    /// this class provides methods for interacting with items in the database (admin only)
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class ItemController : Controller
    {
        private IItemRepository _repo;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        public ItemController(IItemRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// this method displays a list of all items by type
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_repo.ReadAllItems());
        }

        /// <summary>
        /// this is the item create get method which displays a form allowing an
        /// admin to type in the new item properties
        /// </summary>
        /// <returns></returns>
        public IActionResult Create(bool? error)
        {
            ViewBag.errorMessage = "";

            if (error == true)
                ViewBag.errorMessage = "This type of item already exists";

            return View();
        }

        /// <summary>
        /// this is the post create item method which processes the newly created
        /// object from the admin
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name, Type, Price, AmountInStock")]Item item)
        {
            var itemList = _repo.ReadAllItems();

            if (ModelState.IsValid)
            {
                foreach (var iTem in itemList)
                {
                    if (iTem.Name == item.Name)
                    {
                        return RedirectToAction("Create", "Item", new { error = true });
                    }
                }
                _repo.CreateItem(item);
                return RedirectToAction("Index", "Item");
            }
            return View();
        }

        /// <summary>
        /// this method displays a view with property details for a specific item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var item = _repo.ReadItem(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }

        /// <summary>
        /// this is the item edit get method which displays a view allowing an admin
        /// to edit an item's properties
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var item = _repo.ReadItem(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            return View(item);
        }

        /// <summary>
        /// this is the post edit item method which processes the changes that the admin
        /// makes to a profile
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id, Name, Type, Price, AmountInStock")]Item item)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateItem(item.Id, item);
                return RedirectToAction("Index", "Item");
            }
            return View(item);
        }

        /// <summary>
        /// this is the delete profile get method which displays a confirmation view to the
        /// admin confirming if he/she wishes to delete that item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            var item = _repo.ReadItem(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            return View(item);
        }

        /// <summary>
        /// this is the delete item post method which processes the admin's decision to
        /// delete an item from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([Bind("Id, Name, Type, Price, AmountInStock")]int id)
        {
            _repo.DeleteItem(id);
            return RedirectToAction("Index", "Item");
        }
    }

}