namespace AirlinesReservationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Plane ID")]
        public int plane_id { get; set; }

        [DisplayName("From")]
        public int from_airport { get; set; }

        [DisplayName("To")]
        public int to_airport { get; set; }

        [DisplayName("Departures At")]
        [DataType(DataType.DateTime)]
        public DateTime departures_at { get; set; }

        [DisplayName("Arrivals At")]
        [DataType(DataType.DateTime)]
        public DateTime arrivals_at { get; set; }

        [DisplayName("Cost")]
        public int cost { get; set; }

        [DisplayName("From")]
        public virtual AirPort AirPort { get; set; }

        [DisplayName("To")]
        public virtual AirPort AirPort1 { get; set; }

        [DisplayName("Plane")]
        public virtual Plane Plane { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketManager> TicketManagers { get; set; }
    }
}
