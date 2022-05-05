using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.Models;
using AirlinesReservationSystem.Helper;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    public class SecurityController : Controller
    {
        private Model1 db = new Model1();
        // GET: Admin/Security
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User _user)
        {
            var user = db.Users.Where(s => s.email == _user.email).SingleOrDefault();
            if(user != null)
            {
                if(user.password == _user.password)
                {
                    AuthHelper.setIdentity(user);
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ModelState.AddModelError("password", "Invalid password.");
                }
            }
            else
            {
                ModelState.AddModelError("email", "Email not found.");
            }
            return View();
        }
    }
}