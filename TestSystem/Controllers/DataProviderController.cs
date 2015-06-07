using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.DataAccess;

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
    }
}