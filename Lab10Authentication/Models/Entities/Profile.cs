using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Models.Entities
{
    public class Profile
    {
        [Key, Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required"), MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required"), MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Street Address is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "2-Character State code is required"), StringLength(2)]
        public string State { get; set; }

        [Required(ErrorMessage = "5-digit Zip code is required"), StringLength(5)]
        public string Zip { get; set; }

        public ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
