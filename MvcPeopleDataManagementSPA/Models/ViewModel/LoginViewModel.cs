using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; }     

        [Required]
        [DataType(DataType.Password)]
        [StringLength(60, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
