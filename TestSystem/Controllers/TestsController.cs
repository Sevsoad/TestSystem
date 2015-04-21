using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestSystem.Controllers
{
    public class TestsController : Controller
    {
        [Authorize]
        public ActionResult Algorithms()
        {
            return View();
        }

        [Authorize]
        public ActionResult TestSets()
        {
            return View();
        }
    }
}