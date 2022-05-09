using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlinesReservationSystem.Models.Form
{
    public class OrderTicketForm
    {
        public string from { get; set; }
        public string to { get; set; }
        public string repartureDate { get; set; }
        public string returnDate { get; set; }
    }
}