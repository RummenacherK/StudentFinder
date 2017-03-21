using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFinder.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name" )]
        public string fName { get; set; }

        [Display(Name = "Last Name")]
        public string lName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "User Role")]
        public string RoleNames { get; set; }

        public string SchoolId { get; set; }
    }
}
