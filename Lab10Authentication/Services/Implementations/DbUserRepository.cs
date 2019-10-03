using Lab10Authentication.Data;
using Lab10Authentication.Models;
using Lab10Authentication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services.Implementations
{
    /// <summary>
    /// this class implements the IUserRepository
    /// </summary>
    public class DbUserRepository : IUserRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="db"></param>
        public DbUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// this method reads in the user's email and returns the ApplicationUser
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApplicationUser ReadUser(string email)
        {
            return _db.Users.Include(p => p.Profile).FirstOrDefault(u => u.Email == email);
        }
    }
}
