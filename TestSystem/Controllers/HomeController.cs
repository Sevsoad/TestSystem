using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestSystem.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
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

        public ActionResult ErrorPage()
        {
            return View();
        }
    }
}