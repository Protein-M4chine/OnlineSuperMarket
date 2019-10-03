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
    [Authorize(Roles = "Admin")]
    public class ItemController : Controller
    {
        private IItemRepository _repo;

        public ItemController(IItemRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.ReadAllItems());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name, Type, Price, AmountInStock")]Item item)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateItem(item);
                return RedirectToAction("Index", "Item");
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            var item = _repo.ReadItem(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            var item = _repo.ReadItem(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            return View(item);
        }

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

        public IActionResult Delete(int id)
        {
            var item = _repo.ReadItem(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([Bind("Id, Name, Type, Price, AmountInStock")]int id)
        {
            _repo.DeleteItem(id);
            return RedirectToAction("Index", "Item");
        }
    }

}