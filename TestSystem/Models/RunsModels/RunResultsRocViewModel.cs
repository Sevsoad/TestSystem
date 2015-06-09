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

        [Display(Name = "Average TP number")]
        public string TruePositivesNumber { get; set; }

        [Display(Name = "Average TN number")]
        public string TrueNegativesNumber { get; set; }

        [Display(Name = "Average FP number")]
        public string FalsePositivesNumber { get; set; }

        [Display(Name = "Average FN number")]
        public string FalseNegativesNumber { get; set; }

        [Display(Name = "Average correct rate")]
        public string CorrectRate { get; set; }

        [Display(Name = "Teach-test ratio")]
        public string Ratio { get; set; }

        [Display(Name = "Roc positive class")]
        public string RocClass { get; set; }
    }
}