using Lab10Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services.Interfaces
{
    /// <summary>
    /// this interface is used to interact with the ApplicationUser class
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// this method signature reads in the user's email and returns the ApplicationUser
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        ApplicationUser ReadUser(string email);
    }
}
