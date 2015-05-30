using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TestSystem.Core;
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
        }

        [Authorize]
        [HttpPost]
        public ActionResult StartTestRun(RunDetailsViewModel runDetails)
        {
            var regex = new Regex("^([0-9]|[0-4][0]|[0-3][0-9])$");

            if (!ModelState.IsValid)
            {
                return View(runDetails);
            }
            if (runDetails.RepeatNumber == null ||
                !regex.IsMatch(runDetails.RepeatNumber))
            {
                ViewBag.ReRun = "Re-run number is invalid";
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
                        (x => x.Name.ToLower() == runDetails.TestName).Id,
                        RocCurveCalc = runDetails.RocCurveRequired == "true" ? true : false,
                        Status = "In progress",
                        ReTeachNum = Convert.ToInt32(runDetails.RepeatNumber)
                    };

                    context.TestRuns.Add(testRun);
                    context.SaveChanges();

                    var currentRun = context.TestRuns.Where(x => (x.UserId == userId)).OrderByDescending(x => x.Id).ToArray()[0];
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

            return RedirectToAction("UploadResultsFile", "Runs", new { runId = runDetails.RunNumber });
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadResultsFile(int runId)
        {
            var model = new UploadResultViewModel();

            using (var context = new Entities())
            {
                var run = context.TestRuns.Find(runId);
                model.RunNumber = runId;
                model.TestName = context.TestSets.Find(run.TestSetId).Name;
                model.AlgorithmName = context.Algorithms.Find(run.AlgorithmId).Name;
                model.RepeatNumber = (run.ReTeachNum == null ||
                    run.ReTeachNum == 0) ? "no re-runs"
                    : run.ReTeachNum.ToString();
                model.RocCalc = run.RocCurveCalc == true ? "yes" : "no";
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadResultsFile(UploadResultViewModel model)
        {
            var regex = new Regex("^([0-9]|[0-9][0-9]|[0-9][0-9][0-9]|[0-9][0-9][0-9][0-9]|[0-9][0-9][0-9][0-9][0-9])$");

            if (model.RocCalc == "yes" && !regex.IsMatch(model.RocClassNumber))
            {
                ModelState.AddModelError("CustomErrorRoc", "Incorrect class number.");
                return View(model);
            }

            if (model.file == null)
            {
                ModelState.AddModelError("CustomError", "Please, attach test results file.");
                return View(model);
            }
            else
            {
                using (var context = new Entities())
                {
                    var run = context.TestRuns.Find(model.RunNumber);

                    var results = new TestResults()
                    {
                        AlgorithmId = context.Algorithms
                        .Where(x => x.Name == model.AlgorithmName).ToArray()[0].Id,
                        TestRunId = model.RunNumber
                    };

                    var test = context.TestSets.Where(x => x.Name == model.TestName).ToArray()[0];
                    try
                    {
                        var analyzer = new TestResultsAnalyzer();
                        if (model.RocCalc == "no")
                        {
                            using (MemoryStream ms = new MemoryStream())
                            { //todo check format
                                model.file.InputStream.CopyTo(ms);
                                ms.Seek(0, SeekOrigin.Begin);

                                var statistics = analyzer.CalcErrorPercentage(test.ExpectedResults, ms);

                                results.ErrorRate = statistics.ToString("0.00");
                                context.TestResults.Add(results);

                                context.SaveChanges();

                                run.Status = "Results saved";

                                context.SaveChanges();

                                return RedirectToAction("RunResults", new { resultsId = results.Id });
                            }
                        }
                        else if (model.RocCalc == "yes")
                        {
                            using (MemoryStream ms = new MemoryStream())
                            { //todo check format
                                model.file.InputStream.CopyTo(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                var rocCreator = new RocCurveCreator();
                                var sbSensivity = new StringBuilder();
                                var sbSpecifity = new StringBuilder();

                                var statistics = rocCreator.GenerateRocCurveCoordinates(test.ExpectedResults, ms, model.RocClassNumber);

                                for (var i = 0; i < statistics.rocCoordinatesSensivity.Count(); i++)
                                {
                                    sbSensivity.Append(statistics.rocCoordinatesSensivity[i].ToString("0.00") + " ");
                                    sbSpecifity.Append(statistics.rocCoordinatesSpecifity[i].ToString("0.00") + " ");
                                }

                                results.RocCoordinatesSensiv = sbSensivity.ToString();
                                results.RocCoordinatesSpecif = sbSpecifity.ToString();
                                results.FN = statistics.FalseNegativeNumber.Average().ToString("0.00");
                                results.FP = statistics.FalsePositiveNumber.Average().ToString("0.00");
                                results.TN = statistics.TrueNegativeNumber.Average().ToString("0.00");
                                results.TP = statistics.TruePositiveNumber.Average().ToString("0.00");

                                context.TestResults.Add(results);

                                context.SaveChanges();

                                run.Status = "Results saved";

                                context.SaveChanges();

                                return RedirectToAction("RunResults", new { resultsId = results.Id });
                            }

                            run.Status = "Results saved";
                            run.RocClassNumber = model.RocClassNumber;
                            context.SaveChanges();
                        }

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("FileFormatException", "Error occured in results processing. Please check file format.");
                        return View(model);
                    }
                }
            }

            ModelState.AddModelError("FileFormatException", "Server error.");
            return View(model);
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

        [Authorize]
        public ActionResult RunResults(int resultsID)
        {
            return View();
        }
    }
}