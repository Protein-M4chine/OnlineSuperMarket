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
    /// this class implements the ICartItemRepository
    /// </summary>
    public class DbCartItemRepository : ICartItemRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="db"></param>
        public DbCartItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// this method takes in a cartItem and creates it in the database
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public CartItem CreateCartItem(CartItem cartItem)
        {
            _db.CartItems.Add(cartItem);
            _db.SaveChanges();
            return cartItem;
        }

        /// <summary>
        /// this method takes in an id and returns the respective CartItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CartItem ReadCartItem(int id)
        {
            return _db.CartItems.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// this method returns an ICollection of CartItems from the database
        /// </summary>
        /// <returns></returns>
        public ICollection<CartItem> ReadAllCartItems()
        {
            return _db.CartItems.ToList();
        }

        /// <summary>
        /// this method takes in an id and a CartItem, modifies the CartItem, and updates the database from it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartItem"></param>
        public void UpdateCartItem(int id, CartItem cartItem)
        {
            _db.Entry(cartItem).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// this method takes in an id and deletes the respective CartItem from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCartItem(int id)
        {
            CartItem cartItem = _db.CartItems.Find(id);
            _db.CartItems.Remove(cartItem);
            _db.SaveChanges();
        }
    }
}
