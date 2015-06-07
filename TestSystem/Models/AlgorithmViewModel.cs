using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class AlgorithmViewModel
    {
        [Required]
        [Display(Name = "Algorithm name")]
        [StringLength(30, MinimumLength = 2)]
        public string AlgorithmName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Algorithm description")]
        [StringLength(300, MinimumLength = 5)]
        public string AlgorithmDescription { get; set; }
    }
}