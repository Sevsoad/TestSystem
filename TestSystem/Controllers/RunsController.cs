using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.Models.RunsModels;

namespace TestSystem.Controllers
{
    public class RunsController : Controller
    {
        public ActionResult RunResults()
        {
            return View(new RunResultsViewModel { FalseNegativesNumber = "1", FalsePositivesNumber = "3", TruePositivesNumber = "94", TrueNegativesNumber = "70" });
        }
    }
}