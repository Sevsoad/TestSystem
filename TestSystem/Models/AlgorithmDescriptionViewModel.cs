using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class AlgorithmDescriptionViewModel
    {
        [Display(Name = "Algorithm name")]
        public string AlgorithmName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Creation date")]
        public string DateCreated { get; set; }

        [Display(Name = "Last run")]
        public string LastRun { get; set; }

        [Display(Name = "Total runs")]
        public string Runs { get; set; }
    }
}