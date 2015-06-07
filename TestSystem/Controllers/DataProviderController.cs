using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.DataAccess;
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
        public JsonResult GetGridCount(string type)
        {
            var count = 0;

            if (type == "Runs")
            {
                using (var context = new Entities())
                {
                    count = context.TestRuns.Count();
                }
            }

            return Json(count, JsonRequestBehavior.AllowGet);
        }
    }
}