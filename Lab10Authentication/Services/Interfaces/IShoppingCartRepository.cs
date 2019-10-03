using Lab10Authentication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services.Interfaces
{
    /// <summary>
    /// this interface defines methods for creating and reading shopping carts
    /// </summary>
    public interface IShoppingCartRepository
    {
        /// <summary>
        /// this method signature reads in a ShoppingCart, creates a new one in the database, and returns it
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        ShoppingCart CreateShoppingCart(ShoppingCart shoppingCart);

        /// <summary>
        /// this method signature reads in the ApplicationUserId and returns the respective ShoppingCart
        /// </summary>
        /// <param name="applicationUserId"></param>
        /// <returns></returns>
        ShoppingCart ReadShoppingCart(string applicationUserId);
    }
}
