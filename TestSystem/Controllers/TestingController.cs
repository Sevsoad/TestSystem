using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestSystem.DataAccess;
using TestSystem.Models;

namespace TestSystem.Controllers
{
    public class TestingController : Controller
    {

        [HttpGet]
        public ActionResult Algorithms()
        {
            List<AlgorithmDescriptionViewModel> algorithmList = new List<AlgorithmDescriptionViewModel>();

            using (var context = new Entities())
            {
                var list = context.Algorithms.Take(10);
                foreach (var algorithm in list)
                {
                    var lastAlgRun = string.Empty;

                    if (context.TestRuns.Where(x => x.AlgorithmId == algorithm.Id).Count() != 0)
                    {
                        lastAlgRun = context.TestRuns.Where(x => x.AlgorithmId == algorithm.Id).
                            OrderByDescending(p => p.DateOfRun).Take(1).ToList()
                            .Last().DateOfRun.ToString();
                    }
                    else
                    {
                        lastAlgRun = "never";
                    }

                    var shortDescription = string.Empty;
                    if (algorithm.Description.Length > 25)
                    {
                        shortDescription = algorithm.Description.Substring(0, 12) + "...";
                    }
                    else
                    {
                        shortDescription = algorithm.Description;
                    }

                    algorithmList.Add(new AlgorithmDescriptionViewModel
                    {
                        CreatedBy = context.Users.FirstOrDefault<Users>(x => x.Id == algorithm.CreatorId).UserName,
                        DateCreated = algorithm.DateOfCreation.ToString(),
                        Description = shortDescription,
                        LastRun = lastAlgRun,
                        Runs = context.TestRuns.Count<TestRuns>(x => x.AlgorithmId == algorithm.Id).ToString(),
                        AlgorithmName = algorithm.Name
                    });
                }
            }
                //redirect to runs associated with current algoritm ?? see runs in details?
            return View(algorithmList);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateAlgorithm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateAlgorithm(AlgorithmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var context = new Entities())
            {
                if (context.Algorithms.FirstOrDefault<Algorithms>(x => x.Name.ToLower() == model.AlgorithmName) != null)
                {
                    ModelState.AddModelError("AlgorithmName", "This algorithm name is already taken.");
                    return View(model);
                }

                context.Algorithms.Add(new Algorithms
                {
                    Name = model.AlgorithmName,
                    Description = model.AlgorithmDescription,
                    CreatorId = context.Users.FirstOrDefault<Users>
                    (x => x.UserName.ToLower() == User.Identity.Name.ToLower()).Id,
                    DateOfCreation = DateTime.Now
                });

                context.SaveChanges();
            }


            return RedirectToAction("Algorithms", "Testing");
        }

        public ActionResult TestSets()
        {
            List<TestDescriptionViewModel> testList = new List<TestDescriptionViewModel>();

            using (var context = new Entities())
            {
                var list = context.TestSets.OrderByDescending(p => p.DateOfCreation).Take(10);

                foreach (var test in list)
                {
                    var lastTestRun = string.Empty;

                    if (context.TestRuns.Where(x => x.TestSetId == test.Id).Count() != 0)
                    {
                        lastTestRun = context.TestRuns.Where(x => x.TestSetId == test.Id).
                            OrderByDescending(p => p.DateOfRun).Take(1).ToList()
                            .Last().DateOfRun.ToString();
                    }
                    else
                    {
                        lastTestRun = "never";
                    }

                    var shortDescription = string.Empty;
                    if (test.Description.Length > 25)
                    {
                        shortDescription = test.Description.Substring(0, 12) + "...";
                    }
                    else
                    {
                        shortDescription = test.Description;
                    }

                    var fileSize = string.Empty;
                    fileSize = test.Size == 0 ? "< 1" : test.Size.ToString();

                    testList.Add(new TestDescriptionViewModel
                    {
                        CreatedBy = context.Users.FirstOrDefault<Users>(x => x.Id == test.CreatorId).UserName,
                        DateCreated = test.DateOfCreation.ToString(),
                        Description = shortDescription,
                        FileSize = fileSize + " kb",
                        LastRun = lastTestRun,
                        Runs = context.TestRuns.Count(x => x.TestSetId == test.Id).ToString(),
                        TestName = test.Name
                    });
                }
            }

            return View(testList);
        }      

        [Authorize]
        [HttpGet]
        public ActionResult UploadTest()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadTest(UploadTestViewModel model)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }           

            using (var context = new Entities())
            {
                if (context.TestSets.FirstOrDefault(x => x.Name.ToLower()
                    == model.TestName) != null)
                {
                    ModelState.AddModelError("TestName",
                        "This test set name is already taken.");
                    return View(model);
                }

                byte[] array;
                var contentType = model.file.ContentType;

                using (MemoryStream ms = new MemoryStream())
                {
                    model.file.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                }

                var fileSize = model.file.ContentLength/1024;
                var testSet = new TestSets
                {
                    CreatorId = context.Users.FirstOrDefault<Users>
                    (x => x.UserName.ToLower() == User.Identity.Name.ToLower()).Id,
                    TotalRuns = 0,
                    DateOfCreation = DateTime.Now,
                    Description = model.Description,
                    Data = array,
                    Name = model.TestName,
                    Size = fileSize
                };

                context.TestSets.Add(testSet);

                context.SaveChanges();
            }

            return RedirectToAction("TestSets", "Testing");
        }
    }
}