using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace TestSystem.Models.RunsModels
{
    public class RunResultsRocViewModel
    {
        [Display(Name = "Run ID")]
        public string RunNumber { get; set; }

        [Required]
        [Display(Name = "Test name")]
        public string TestName { get; set; }

        [Required]
        [Display(Name = "Algorithm name")]
        public string AlgorithmName { get; set; }

        [Display(Name = "Average true positive number")]
        public string TruePositivesNumber { get; set; }

        [Display(Name = "Average true negative number")]
        public string TrueNegativesNumber { get; set; }

        [Display(Name = "Average false positive number")]
        public string FalsePositivesNumber { get; set; }

        [Display(Name = "Average false negative number")]
        public string FalseNegativesNumber { get; set; }

        [Display(Name = "Average correct classification rate")]
        public string CorrectRate { get; set; }

        //public Json RocCoorinates { get; set; }

        //public string RocCoorinatesSpecif { get; set; }
    }
}