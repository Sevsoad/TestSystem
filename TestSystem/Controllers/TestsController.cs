using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.DataAccess;

namespace TestSystem.Controllers
{
    public class TestsController : Controller
    {
        //[Authorize]
        public ActionResult Algorithms()
        {
            return View();
        }

        //[Authorize]
        public ActionResult TestSets()
        {
            return View();
        }

        public ActionResult DownloadTestFile()
        {
            TestSets testSet;

            using (var context = new Entities())
            {

                testSet = context.TestSets.Find(1);

            }


            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "funny test set2.txt",
                Inline = false
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            
            // View document
            return File(testSet.Data, "text/plain");
        }


        [HttpPost]
        public ActionResult UploadTestFile(HttpPostedFileBase file)
        {

            byte[] array;
            var contentType = file.ContentType;

            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                array = ms.GetBuffer();                
            }

            using (var context = new Entities())
            {
                var testSet = new TestSets { Users = context.Users.FirstOrDefault<Users>(x => x.UserName.ToLower() == "user1"),
                TotalRuns = 0, DateOfCreation = DateTime.Now,
                Data = array};

                context.TestSets.Add(testSet);

                context.SaveChanges();
            }

            return RedirectToAction("TestSets", "Tests");
        }
    }
}