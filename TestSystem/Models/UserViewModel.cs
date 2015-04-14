using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Your username*")]
        [StringLength(10, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 3)]
        [Display(Name = "Password*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password*")]
        [Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(30)]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Email*")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Format of email address is wrong.")]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string Company { get; set; }

        [StringLength(50)]
        [Display(Name = "Other information")]
        public string OtherDetails { get; set; }
    }
}