using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.DataAccess;
using TestSystem.Models;

namespace TestSystem.Controllers
{
    public class LoginController : Controller
    {
        private string cacheKey = "courses";
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            using (var context = new Entities())
            {
                context.Users.Add(new Users {UserName = input.UserName,
                    Password = input.Password, Roles = context.Roles.Find(2)});
                context.SaveChanges();            
            }

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            //HttpContext.Cache.Remove(cacheKey);
            return RedirectToAction("Index", "Home");
        }
    }
}