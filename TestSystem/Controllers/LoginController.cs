using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestSystem.DataAccess;
using TestSystem.Models;

namespace TestSystem.Controllers
{
    public class LoginController : Controller
    {
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
                if (context.Users.FirstOrDefault<Users>(x => x.UserName.ToLower() == input.UserName) != null)
                {
                    ModelState.AddModelError("UserName", "This username is already registered.");
                    return View(input);
                }

                var user = new Users
                {
                    UserName = input.UserName.ToLower(),
                    Roles = context.Roles.Find(2)//fix role
                };

                context.Users.Add(user);
                context.SaveChanges();

                var userDetails = new UserInfo
                {
                    Id = context.Users.FirstOrDefault<Users>(x => x.UserName == input.UserName.ToLower()).Id,
                    Company = input.Company,
                    Email = input.Email,
                    FullName = input.FullName,
                    Other = input.OtherDetails
                };

                context.UserInfo.Add(userDetails);
                context.SaveChanges();
            }
            //redirect to login + message
            return RedirectToAction("Login", "Login");
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
                if (context.Users.FirstOrDefault<Users>(x => x.UserName.ToLower() == input.UserName) == null
                   || context.Users.FirstOrDefault<Users>(x => x.UserName.ToLower() == input.UserName)
                   .Password != input.Password)
                {
                    ModelState.AddModelError("UserName", "Username - password combination don't match.");
                    return View(input);
                }
                FormsAuthentication.SetAuthCookie(input.UserName, true);               
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}