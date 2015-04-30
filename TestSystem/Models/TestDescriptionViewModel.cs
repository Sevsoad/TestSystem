using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class TestDescriptionViewModel
    {
        public string TestName { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string DateCreated { get; set; }

        public string FileSize { get; set; }

        public string LastRun { get; set; }

        public string Runs { get; set; }
    }
}