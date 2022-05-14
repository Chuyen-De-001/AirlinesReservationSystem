﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.Models.Form;
using AirlinesReservationSystem.Models;
using AirlinesReservationSystem.Helper;
using System.Data.Entity.Core.Objects;
using Newtonsoft.Json;
using System.Data.Entity;

namespace AirlinesReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        [HttpGet]
        //[ValidateAntiForgeryToken]


        public ActionResult View()
        {
            ViewBag.name = "hai";
            return PartialView();
        }

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

        public ActionResult DetailFlightSchedule(int id)
        {
            FlightSchedule flightSchedule = db.FlightSchedules.Where(s => s.id == id).FirstOrDefault();
            if(flightSchedule == null)
            {
                return HttpNotFound();
            }
            return PartialView(flightSchedule);
        }

        [HttpGet]
        public ActionResult PayTicket(string ticketID,int flightScheduleID)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            response["status"] = "200";
            response["message"] = "";
            if (!AuthHelper.isLogin())
            {
                response["status"] = "400";
                response["message"] = "Must login before buying tickets.";
                return Content(JsonConvert.SerializeObject(response));
            }
            TicketManager ticket = new TicketManager();
            ticket.user_id = AuthHelper.getIdentity().id;
            ticket.flight_schedules_id = flightScheduleID;
            ticket.status = TicketManager.STATUS_PAY;
            ticket.code = ticketID;
            if (ModelState.IsValid)
            {
                db.TicketManagers.Add(ticket);
                db.SaveChanges();
                AlertHelper.setToast("success", "Order ticket successfully.");

            }
            return Content(JsonConvert.SerializeObject(response));


        }

        public ActionResult YourTicket()
        {
            if (!AuthHelper.isLogin())
            {
                return RedirectToAction("Index");
            }
            User user = AuthHelper.getIdentity();
            IEnumerable<TicketManager> ticketManagers = db.TicketManagers.Where(s => s.user_id == user.id).ToList();
            return View(ticketManagers);
        }

        public ActionResult DetailTicket(int id)
        {
            TicketManager ticket = db.TicketManagers.Where(s => s.id == id).FirstOrDefault();
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return PartialView(ticket);
        }

        public ActionResult CancelTicket(int id)
        {
            TicketManager ticket = db.TicketManagers.Where(s => s.id == id).FirstOrDefault();
            if(ticket == null)
            {
                return HttpNotFound();
            }
            ticket.status = TicketManager.STATUS_CANCEL;
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                AlertHelper.setToast("warning", "Cancel ticket successfull.");
            }
            return RedirectToAction("YourTicket", "Home");
        }
    }
}