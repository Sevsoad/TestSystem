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

            using (var context = new Entities())
            {
                if (context.Algorithms.Where
                    (x => x.Name == runDetails.AlgorithmName).Count() == 0)
                {
                    ModelState.AddModelError("AlgorithmName",
                   "Can't find this algorithm.");
                    return View(runDetails);
                }
                var testSet = context.TestSets.Where
                    (x => x.Name == runDetails.TestName);

                if (testSet.Count() == 0)
                {
                    ModelState.AddModelError("TestName",
                       "Can't find this test set.");
                    return View(runDetails);
                }
                else
                {
                    var date = DateTime.Now;
                    var userId = context.Users.FirstOrDefault<Users>
                    (x => x.UserName.ToLower() == User.Identity.Name.ToLower()).Id;
                    var testRun = new TestRuns()
                    {
                        DateOfRun = date,
                        UserId = userId,
                        AlgorithmId = context.Algorithms.FirstOrDefault
                        (x => x.Name.ToLower() == runDetails.AlgorithmName).Id,
                        TestSetId = context.TestSets.FirstOrDefault
                        (x => x.Name.ToLower() == runDetails.TestName).Id
                    };

                    context.TestRuns.Add(testRun);
                    context.SaveChanges();

                    var currentRun = context.TestRuns.Where(x => (x.UserId == userId)).OrderByDescending(x=> x.Id).ToArray()[0];
                    if (currentRun != null)
                    {
                        runDetails.RunNumber = currentRun.Id;
                    }
                    else
                    {                        
                        return RedirectToAction("ErrorPage", "Home");
                    }       
                }
            }

            return RedirectToAction("UploadResultsFile", "Runs", new { runId = runDetails.RunNumber});
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadResultsFile(int runId)
        {
            var model = new RunDetailsViewModel();

            using (var context = new Entities())
            {
                var run = context.TestRuns.Find(runId);
                model.RunNumber = runId;
                model.Status = "In progress";
                model.TestName = context.TestSets.Find(run.TestSetId).Name;
                model.AlgorithmName = context.Algorithms.Find(run.AlgorithmId).Name;
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadResultsFile(RunDetailsViewModel model)
        {
            if (model.file == null)
            {
                ModelState.AddModelError("CustomError", "Please, attach test results file.");
                return View(model);
            }

            return View();
        }
        

        [Authorize]
        [HttpGet]
        public ActionResult DownloadTestFile(string testName)
        {
            byte[] testFile = null;
            using (var context = new Entities())
            {                
                var testSet = context.TestSets.Where
                    (x => x.Name == testName);

                if (testSet.Count() == 0)
                {                    
                    return View();
                }
                else
                {
                    testFile = testSet.ToArray()[0].Data;
                }
            }

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "TestSet-" + testName + ".txt",
                Inline = false
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(testFile, "text/plain"); 
        }

        public ActionResult RunResults()
        {
            return View();
        }
    }
}