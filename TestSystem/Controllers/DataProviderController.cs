using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.DataAccess;
using TestSystem.Models;
using TestSystem.Models.RunsModels;

namespace TestSystem.Controllers
{
    public class DataProviderController : Controller
    {
        [HttpGet]
        public JsonResult AutocompleteTestName(string term)
        {
            List<string> result = new List<string>();

            using (var context = new Entities())
            {
                var tests = context.TestSets.Where(x => x.Name.Contains(term)).Take(10);

                foreach (var test in tests)
                {
                    result.Add(test.Name);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AutocompleteAlgName(string term)
        {
            List<string> result = new List<string>();

            using (var context = new Entities())
            {
                var algs = context.Algorithms.Where(x => x.Name.Contains(term)).Take(10);

                foreach (var alg in algs)
                {
                    result.Add(alg.Name);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGridRuns(string page, string size)
        {
            List<AllRunsViewModel> list = new List<AllRunsViewModel>();

            using (var context = new Entities())
            {
                var pageSize = Convert.ToInt32(size);
                var skipSize = pageSize * (Convert.ToInt32(page) - 1);
                var rangeOfRuns = context.TestRuns.OrderByDescending(x => x.Id).Skip(skipSize).Take(pageSize);

                foreach (var run in rangeOfRuns)
                {
                    var testRun = new AllRunsViewModel();

                    testRun.AlgorithmName = context.Algorithms.Find(run.AlgorithmId).Name;
                    testRun.DateStart = run.DateOfRun.ToString();
                    testRun.RocCalc = run.RocCurveCalc.ToString();
                    testRun.RocClass = run.RocClassNumber;
                    testRun.RunNumber = run.Id.ToString();
                    testRun.RunsNumber = run.ReTeachNum.ToString();
                    testRun.Status = run.Status;
                    testRun.TestName = context.TestSets.Find(run.TestSetId).Name;
                    testRun.UserName = context.Users.Find(run.UserId).UserName;

                    list.Add(testRun);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGridTests(string page, string size)
        {
            List<TestDescriptionViewModel> list = new List<TestDescriptionViewModel>();

            using (var context = new Entities())
            {
                var pageSize = Convert.ToInt32(size);
                var skipSize = pageSize * (Convert.ToInt32(page) - 1);
                var rangeOfSets = context.TestSets.OrderByDescending(x => x.Id).Skip(skipSize).Take(pageSize);
                var lastTestRun = string.Empty;

                foreach (var set in rangeOfSets)
                {
                    if (context.TestRuns.Where(x => x.TestSetId == set.Id).Count() != 0)
                    {
                        lastTestRun = context.TestRuns.Where(x => x.TestSetId == set.Id).
                            OrderByDescending(p => p.DateOfRun).Take(1).ToList()
                            .Last().DateOfRun.ToString();
                    }
                    else
                    {
                        lastTestRun = "never";
                    }

                    var shortDescription = string.Empty;
                    if (set.Description.Length > 25)
                    {
                        shortDescription = set.Description.Substring(0, 12) + "...";
                    }
                    else
                    {
                        shortDescription = set.Description;
                    }

                    var fileSize = string.Empty;
                    fileSize = set.Size == 0 ? "< 1" : set.Size.ToString();

                    var testSet = new TestDescriptionViewModel
                    {
                        CreatedBy = context.Users.FirstOrDefault<Users>(x => x.Id == set.CreatorId).UserName,
                        DateCreated = set.DateOfCreation.ToString(),
                        Description = shortDescription,
                        FileSize = fileSize + " kb",
                        LastRun = lastTestRun,
                        Runs = context.TestRuns.Count(x => x.TestSetId == set.Id).ToString(),
                        TestName = set.Name
                    };

                    list.Add(testSet);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGridCount(string type)
        {
            var count = 0;
            
                using (var context = new Entities())
                {
                    if (type == "Runs")
                    {
                        count = context.TestRuns.Count();
                    }
                    else if (type == "Tests")
                    {
                        count = context.TestSets.Count();
                    }
                }            

            return Json(count, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRunResults(string runId)
        {
            var resultsId = 0;
            var runIdInt = Convert.ToInt32(runId);

            using (var context = new Entities())
            {
                var run = context.TestRuns.Find(runIdInt);

                if (run.Status == "In progress")
                {
                    return RedirectToAction("UploadResultsFile", "Runs", new { runId = runIdInt });
                }
                else
                {
                    resultsId = context.TestResults
                    .Where(x => x.TestRunId == runIdInt).ToArray()[0].Id;
                    var isRoc = run.RocCurveCalc;

                    if (isRoc)
                    {
                        return RedirectToAction("RunResultsRoc", "Runs", new { resultsID = resultsId });
                    }
                }              
            }

            return RedirectToAction("RunResults", "Runs", new { resultsID = resultsId});
        }
    }
}