namespace AirlinesReservationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FlightSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FlightSchedule()
        {
            TicketManagers = new HashSet<TicketManager>();
        }

        public int id { get; set; }

        public int plane_id { get; set; }

        public int from_airport { get; set; }

        public int to_airport { get; set; }

        public DateTime departures_at { get; set; }

        public DateTime arrivals_at { get; set; }

        public int cost { get; set; }

        public virtual AirPort AirPort { get; set; }

        public virtual AirPort AirPort1 { get; set; }

        public virtual Plane Plane { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketManager> TicketManagers { get; set; }
    }
}
