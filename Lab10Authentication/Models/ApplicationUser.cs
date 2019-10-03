using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Lab10Authentication.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lab10Authentication.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public ICollection<string> RoleNames { get; set; }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
