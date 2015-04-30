using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.Core;

namespace TestSystem.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult GetCurveCoordinates()
        {
            var rocCurveCreator = new RocCurveCreator();
            var dataWorker = new DataWorker();
            var rocResults = rocCurveCreator.GenerateRocCurveCoordinates( dataWorker.GetExpectedResults(),
                dataWorker.GetTestingResults(), "1");

            var jsonResult = new List<float[]>();
            var jsonResult2 = new List<List<float[]>>();

            for (var i = 0; i < rocResults.rocCoordinatesSensivity.Count; i++)
            {
                jsonResult.Add(new float[] {rocResults.rocCoordinatesSpecifity[i], 
                    rocResults.rocCoordinatesSensivity[i]});
            }

            jsonResult2.Add(jsonResult);
            var defaultCurve = new List<float[]>();
            defaultCurve.Add(new float[] {0, 0});
            defaultCurve.Add(new float[] {1,1});

            jsonResult2.Add(defaultCurve);

            return Json(jsonResult2, JsonRequestBehavior.AllowGet);

        }

        [Authorize] 
        public ActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "Admins")]
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

    }
}