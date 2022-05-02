namespace AirlinesReservationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TicketManager")]
    public partial class TicketManager
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("FlightSchedule")]
        public int flight_schedules_id { get; set; }

        [DisplayName("User")]
        public int user_id { get; set; }

        [DisplayName("Status")]
        public int status { get; set; }

        public virtual FlightSchedule FlightSchedule { get; set; }

        public virtual User User { get; set; }
    }
}
