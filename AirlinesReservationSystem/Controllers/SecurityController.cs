using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace AirlinesReservationSystem.Controllers
{
    public class SecurityController : Controller
    {

        private static Model1 db = new Model1();
        // GET: Security
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp(string email="" ,string password = "")
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            response["status"] = "200";
            response["message"] = "";
            
            if(Models.User.emailExists(email) == true)
            {
                response["status"] = "400";
                response["message"] = "Email already exists.";
            }
            Models.User user = new Models.User();
            user.email = email;
            user.password = password;
            user.user_type = Models.User.TYPE_CUSTOM;
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return Content(JsonConvert.SerializeObject(response));
            
        }

    }
}