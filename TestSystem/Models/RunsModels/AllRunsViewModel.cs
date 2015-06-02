using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models.RunsModels
{
    public class AllRunsViewModel
    {
        [Display(Name = "Run number")]
        public string RunNumber { get; set; }

        [Display(Name = "Algorithm name")]
        public string AlgorithmName { get; set; }

        [Display(Name = "Test name")]
        public string TestName { get; set; }

        [Display(Name = "Run status")]
        public string Status { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        [Display(Name = "Start date")]
        public string DateStart { get; set; }

        [Display(Name = "Roc-curve calculated")]
        public string RocCalc { get; set; }

        [Display(Name = "Roc class number")]
        public string RocClass { get; set; }

        [Display(Name = "Test runs number")]
        public string RunsNumber { get; set; }        
    }
}