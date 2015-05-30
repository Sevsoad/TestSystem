using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSystem.Models.RunsModels
{
    public class UploadResultViewModel
    {
        [Required]
        [Display(Name = "Run ID")]
        public int RunNumber { get; set; }

        [Required]
        [Display(Name = "Test name")]
        public string TestName { get; set; }

        [Required]
        [Display(Name = "Algorithm name")]
        public string AlgorithmName { get; set; }

        [Display(Name = "Roc-curve calculation")]
        public string RocCalc { get; set; }

        [Display(Name = "Number of re-runs")]
        public string RepeatNumber { get; set; }
                
        [Required]
        public HttpPostedFileBase file { get; set; }


        public string RocClassNumber { get; set; }
    }
}