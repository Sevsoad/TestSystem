using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSystem.Models.RunsModels
{
    public class RunResultsViewModel
    {
        public string TruePositivesNumber { get; set; }

        public string TrueNegativesNumber { get; set; }

        public string FalsePositivesNumber { get; set; }

        public string FalseNegativesNumber { get; set; }
    }
}