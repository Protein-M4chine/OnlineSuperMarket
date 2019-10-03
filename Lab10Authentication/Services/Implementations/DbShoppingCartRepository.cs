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
    /// this class implements the IShoppingCartRepository
    /// </summary>
    public class DbShoppingCartRepository : IShoppingCartRepository
    {
        private ApplicationDbContext _db;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="db"></param>
        public DbShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// this method reads in a ShoppingCart, creates a new one in the database, and returns it
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public ShoppingCart CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Add(shoppingCart);
            _db.SaveChanges();
            return shoppingCart;
        }

        /// <summary>
        /// this method reads in the ApplicationUserId and returns the respective ShoppingCart
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        public ShoppingCart ReadShoppingCart(string applicationUserId)
        {
            return _db.ShoppingCarts.FirstOrDefault(s => s.ApplicationUserId == applicationUserId);
        }
    }
}
