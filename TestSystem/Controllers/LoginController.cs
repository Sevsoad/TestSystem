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
        private string cacheKey = "tetsSys";
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            using (var context = new Entities())
            {
                if (context.USERS.FirstOrDefault<USERS>(x => x.UserName.ToLower() == input.UserName) != null)
                {                    
                    ViewBag.UserNameError = "This username is already registered.";
                    return View(input);
                }

                var user = new USERS
                {
                    UserName = input.UserName.ToLower(),
                    Password = input.Password,
                    USER_LOGIN_ROLES = context.USER_LOGIN_ROLES.Find(2)
                };                

                context.USERS.Add(user);
                context.SaveChanges();

                var userDetails = new USER_DETAILS
                {
                    Id = context.USERS.First<USERS>(x => x.UserName == input.UserName.ToLower()).Id,
                    Company = input.Company,
                    Email = input.Email,
                    User_full_name = input.FullName,
                    Other = input.OtherDetails
                };

                context.USER_DETAILS.Add(userDetails);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

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
                if (context.USERS.FirstOrDefault<USERS>(x => x.UserName.ToLower() == input.UserName) == null
                    || context.USERS.FirstOrDefault<USERS>(x => x.UserName.ToLower() == input.UserName)
                    .Password != input.Password)
                {
                    ViewBag.UserNameError = "Username - password combination don't match.";
                    return View(input);
                }       
            }

            return RedirectToAction("Index", "Home");
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