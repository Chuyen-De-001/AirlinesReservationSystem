using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.Models.Form;
using AirlinesReservationSystem.Models;
using System.Data.Entity.Core.Objects;

namespace AirlinesReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(OrderTicketForm _orderTicketForm)
        {
            ViewBag.from = new SelectList(db.AirPorts, "id", "code");
            ViewBag.to = new SelectList(db.AirPorts, "id", "code");
            ViewBag.flightSchedule = null;
            ViewBag.title = "Search Ticket";
            if(Request.QueryString.Count > 0)
            {
                if (ModelState.IsValid)
                {
                    bool flagError = false;

                    if (!_orderTicketForm.checkDestination())
                    {
                        ModelState.AddModelError("to", "The destination must be different from the point of departure");
                        flagError = true;
                    }

                    if(flagError == false)
                    {
                        //Lấy danh sach chuyến bay phù hợp vs thời gian.
                        
                        var query = db.FlightSchedules.Where(s => s.to_airport == _orderTicketForm.to && s.from_airport == _orderTicketForm.from);
                        DateTime repartureDate = DateTime.Parse(_orderTicketForm.repartureDate);
                        query = query.Where(s => EntityFunctions.TruncateTime(s.departures_at) == EntityFunctions.TruncateTime(repartureDate));
                        List<FlightSchedule> models = query.ToList();
                        ViewBag.flightSchedule = models;
                        return View(_orderTicketForm);
                    }

                }
            }
            else
            {
                ModelState.Clear();
            }
            return View(_orderTicketForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(OrderTicketForm _orderTicketForm)
        {
            if (ModelState.IsValid)
            {
                //Lấy danh sach chuyến bay phù hợp vs thời gian.
            }
            ViewBag.from = new SelectList(db.AirPorts, "id", "code");
            ViewBag.to = new SelectList(db.AirPorts, "id", "code");
            return View(_orderTicketForm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}