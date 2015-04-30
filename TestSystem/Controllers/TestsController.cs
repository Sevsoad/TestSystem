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
    public class TestsController : Controller
    {
        //[Authorize]
        public ActionResult Algorithms()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateTestRun()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateTestRun(string id)
        {
            return RedirectToAction("RunResults", "Runs");
        }

        public ActionResult TestSets()
        {

            List<TestDescriptionViewModel> testList = new List<TestDescriptionViewModel>();

            using (var context = new Entities())
            {
                var list = context.TestSets.OrderByDescending(p => p.DateOfCreation).Take(10);
                foreach (var test in list)
                {
                    testList.Add(new TestDescriptionViewModel
                    {
                        CreatedBy = context.Users.FirstOrDefault<Users>(x => x.Id == test.CreatorId).UserName,
                        DateCreated = test.DateOfCreation.ToString(),
                        Description = test.Description,
                        FileSize = "123",
                        LastRun = "never",
                        Runs = "0",
                        TestName = "not avaliable"
                    });
                }
            }

            return View(testList);
        }

        public ActionResult DownloadTestFile()
        {
            TestSets testSet;

            using (var context = new Entities())
            {

                testSet = context.TestSets.Find(4);

            }


            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "test set number 3.txt",
                Inline = false
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            
            // View document
            return File(testSet.Data, "text/plain");
        }

        [Authorize]
        [HttpGet]
        public ActionResult UploadTest()
        {
            
            return View();
        }

        [Authorize]
        [HttpPost]
        //update name
        public ActionResult UploadTest(UploadTestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            byte[] array;
            var contentType = model.file.ContentType;

            using (MemoryStream ms = new MemoryStream())
            {
                model.file.InputStream.CopyTo(ms);
                array = ms.GetBuffer();                
            }

            using (var context = new Entities())
            {
                var testSet = new TestSets
                {
                    CreatorId = context.Users.FirstOrDefault<Users>
                    (x => x.UserName.ToLower() == User.Identity.Name.ToLower()).Id,
                TotalRuns = 0, DateOfCreation = DateTime.Now, Description = model.Description,
                Data = array};
                                
                context.TestSets.Add(testSet);

                context.SaveChanges();
            }

            return RedirectToAction("TestSets", "Tests");
        }
    }
}