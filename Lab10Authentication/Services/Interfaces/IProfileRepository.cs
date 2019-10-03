using Lab10Authentication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services.Interfaces
{
    /// <summary>
    /// this interface defines the crud profile interaction interface
    /// </summary>
    public interface IProfileRepository
    {
        /// <summary>
        /// this method signature takes in a profile and add it to the database
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        Profile CreateProfile(Profile profile);

        /// <summary>
        /// this method signature takes in an email and returns the respective Profile from it
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Profile ReadProfile(string email);

        /// <summary>
        /// this method signature returns an ICollection of Profiles from the database
        /// </summary>
        /// <returns></returns>
        ICollection<Profile> ReadAllProfiles();

        /// <summary>
        /// this method signature takes in a Profile, modifies it, then updates the database from it
        /// </summary>
        /// <param name="profile"></param>
        void UpdateProfile(Profile profile);

        /// <summary>
        /// this method signature takes in an email and deletes the respective Profile from it
        /// </summary>
        /// <param name="email"></param>
        void DeleteProfile(string email);
    }
}
