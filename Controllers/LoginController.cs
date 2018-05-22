using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using DataAccessLayer;
using Models;

namespace MVCHelloWorld2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Authenticate()
        {
            return View("Login");
        }

        public ActionResult Validate() 
        {
            string username = Request.Form["UserName"].Trim();
            string password = Request.Form["Password"].Trim();
            Dal dal = new Dal();
            List<User> users = (from u in dal.Users
                                    where (u.UserName == username)
                                    && (u.Password == password)
                                    select u).ToList<User>();

            if (users.Count == 1) 
            {
                FormsAuthentication.SetAuthCookie("Cookie",true);
                return View("GotoHome");
            }
            else 
            {
                return View("Login");
            }
        }
    }
}