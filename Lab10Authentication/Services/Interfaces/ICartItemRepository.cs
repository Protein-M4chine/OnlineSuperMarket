using Lab10Authentication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services.Interfaces
{
    /// <summary>
    /// this interface defines the CartItem crud interaction interface
    /// </summary>
    public interface ICartItemRepository
    {
        /// <summary>
        /// this method signature takes in a cartItem and creates it in the database
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        CartItem CreateCartItem(CartItem cartItem);

        /// <summary>
        /// this method signature takes in an id and returns the respective CartItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CartItem ReadCartItem(int id);

        /// <summary>
        /// this method signature returns an ICollection of CartItems from the database
        /// </summary>
        /// <returns></returns>
        ICollection<CartItem> ReadAllCartItems();

        /// <summary>
        /// this method signature takes in an id and a CartItem, modifies the CartItem, and updates the database from it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartItem"></param>
        void UpdateCartItem(int id, CartItem cartItem);

        /// <summary>
        /// this method signature takes in an id and deletes the respective CartItem from the database
        /// </summary>
        /// <param name="id"></param>
        void DeleteCartItem(int id);
    }
}
