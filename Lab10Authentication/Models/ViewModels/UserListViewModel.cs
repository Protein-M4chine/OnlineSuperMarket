using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Models.ViewModels
{
    public class UserListViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public int NumberOfRoles { get; set; }
    }
}
