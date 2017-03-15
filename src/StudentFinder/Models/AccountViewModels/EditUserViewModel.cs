using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFinder.Models.AccountViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string fName { get; set; }

        [Display(Name = "Last Name")]
        public string lName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Select a Role")]
        public string Role { get; set; }
    }
}
