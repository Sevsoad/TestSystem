using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models.RunsModels
{
    public class RunResultsViewModel
    {
        [Display(Name = "Run ID")]
        public string RunNumber { get; set; }

        [Display(Name = "Test name")]
        public string TestName { get; set; }

        [Display(Name = "Algorithm name")]
        public string AlgorithmName { get; set; }

        [Display(Name = "Average correct classification rate")]
        public string CorrectRate { get; set; }

        [Display(Name = "Number of runs")]
        public string NumberOfRuns { get; set; }
    }
}