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
    /// this class implements the IProfileRepository
    /// </summary>
    public class DbProfileRepository : IProfileRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="db"></param>
        public DbProfileRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// this method takes in a profile and add it to the database
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public Profile CreateProfile(Profile profile)
        {
            _db.Profiles.Add(profile);
            _db.SaveChanges();
            return profile;
        }

        /// <summary>
        /// this method takes in an email and returns the respective Profile from it
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Profile ReadProfile(string email)
        {
            var profile = _db.Profiles.Find(email);
            return profile;
        }

        /// <summary>
        /// this method returns an ICollection of Profiles from the database
        /// </summary>
        /// <returns></returns>
        public ICollection<Profile> ReadAllProfiles()
        {
            return _db.Profiles.ToList();
        }

        /// <summary>
        /// this method takes in a Profile, modifies it, then updates the database from it
        /// </summary>
        /// <param name="profile"></param>
        public void UpdateProfile(Profile profile)
        {
            _db.Entry(profile).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// this method takes in an email and deletes the respective Profile from it
        /// </summary>
        /// <param name="email"></param>
        public void DeleteProfile(string email)
        {
            Profile profile = _db.Profiles.Find(email);
            _db.Profiles.Remove(profile);
            _db.SaveChanges();
        }
    }
}
