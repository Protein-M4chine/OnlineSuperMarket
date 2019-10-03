using Lab10Authentication.Data;
using Lab10Authentication.Models.Entities;
using Lab10Authentication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services.Implementations
{
    /// <summary>
    /// this class implements the IItemRepository
    /// </summary>
    public class DbItemRepository : IItemRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="db"></param>
        public DbItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// this method takes in an Item and creates it in the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Item CreateItem(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return item;
        }

        /// <summary>
        /// this method takes in an id and returns the respective Item from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Item ReadItem(int id)
        {
            return _db.Items.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// this method returns an ICollection of Items from the database
        /// </summary>
        /// <returns></returns>
        public ICollection<Item> ReadAllItems()
        {
            return _db.Items.ToList();
        }

        /// <summary>
        /// this method takes in an id and Item, modifies the item, and updates the database from it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        public void UpdateItem(int id, Item item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// this method takes in an id and deletes the respective Item from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteItem(int id)
        {
            Item item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
        }
    }
}
