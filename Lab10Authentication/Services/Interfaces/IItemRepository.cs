using Lab10Authentication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services.Interfaces
{
    /// <summary>
    /// this interface defines the Item crud interaction interface
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// this method signature takes in an Item and creates it in the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Item CreateItem(Item item);

        /// <summary>
        /// this method signature takes in an id and returns the respective Item from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Item ReadItem(int id);

        /// <summary>
        /// this method signature returns an ICollection of Items from the database
        /// </summary>
        /// <returns></returns>
        ICollection<Item> ReadAllItems();

        /// <summary>
        /// this method signature takes in an id and Item, modifies the item, and updates the database from it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        void UpdateItem(int id, Item item);

        /// <summary>
        /// this method signature takes in an id and deletes the respective Item from the database
        /// </summary>
        /// <param name="id"></param>
        void DeleteItem(int id);
    }
}
