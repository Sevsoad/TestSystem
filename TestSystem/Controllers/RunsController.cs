using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.DataAccess;
using TestSystem.Models.RunsModels;

namespace TestSystem.Controllers
{
    public class RunsController : Controller
    {
        [Authorize]
        public ActionResult StartTestRun()
        {
            return View();
            // (new RunResultsViewModel { FalseNegativesNumber = "1", FalsePositivesNumber = "3", TruePositivesNumber = "94", TrueNegativesNumber = "70" });
        }

        [Authorize]
        [HttpPost]
        public ActionResult StartTestRun(RunDetailsViewModel runDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(runDetails);
            }

            byte[] testFile = null;

            using (var context = new Entities())
            {
                if (context.Algorithms.Where
                    (x => x.Name == runDetails.AlgorithmName).Count() == 0)
                {
                    ModelState.AddModelError("AlgorithmName",
                   "Can't find such algorithm name.");
                    return View(runDetails);
                }
                var testSet = context.TestSets.Where
                    (x => x.Name == runDetails.TestName);

                if (testSet.Count() == 0)
                {
                    ModelState.AddModelError("TestName",
                       "Can't find such test set name.");
                    return View(runDetails);
                }
                else
                {
                    testFile = testSet.ToArray()[0].Data;
                    var testRun = new TestRuns()
                    {
                        DateOfRun = DateTime.Now,
                        UserId = context.Users.FirstOrDefault<Users>
                    (x => x.UserName.ToLower() == User.Identity.Name.ToLower()).Id,
                        AlgorithmId = context.Algorithms.FirstOrDefault
                        (x => x.Name.ToLower() == runDetails.AlgorithmName).Id,
                        TestSetId = context.TestSets.FirstOrDefault
                        (x => x.Name.ToLower() == runDetails.TestName).Id
                    };

                    context.TestRuns.Add(testRun);
                    context.SaveChanges();
                }
            }

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "TestRun-" + runDetails.AlgorithmName
                + "-" + runDetails.TestName + ".txt",
                Inline = false
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            //redirect to page for waiting results????
            return File(testFile, "text/plain");
        }

        public ActionResult TestRunResults()
        {
            return View();
        }
    }
}