using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models.RunsModels
{
    public class RunDetailsViewModel
    {
        [Display(Name = "Run ID")]
        public int RunNumber { get; set; }

        [Required]
        [Display(Name = "Test name")]
        public string TestName { get; set; }

        [Required]
        [Display(Name = "Algorithm name")]
        public string AlgorithmName { get; set; }

        public string RunsNumber { get; set; }

        [Display(Name = "Roc-curve calculation")]
        public string RocCurveRequired { get; set; }

        public string Ratio { get; set; }
    }
}