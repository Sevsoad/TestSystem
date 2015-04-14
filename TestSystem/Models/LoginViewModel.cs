using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class LoginViewModel
    {
            [Required]
            [StringLength(10, MinimumLength = 3)]
            public string UserName { get; set; }

            [Required]
            [StringLength(9, MinimumLength = 3)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        
    }
}