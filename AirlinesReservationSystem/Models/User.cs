namespace AirlinesReservationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("User")]
    public partial class User
    {
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            TicketManagers = new HashSet<TicketManager>();
        }

        [DisplayName("ID")]
        public int id { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [DisplayName("Email")]
        [StringLength(255)]
        public string email { get; set; }

        [Required]
        [DisplayName("CCCD")]
        [StringLength(255)]
        public string cccd { get; set; }

        [Required]
        [DisplayName("Address")]
        [StringLength(255)]
        public string address { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [StringLength(255)]
        public string phone_number { get; set; }

        [Required]
        [DisplayName("Password")]
        [StringLength(255)]
        public string password { get; set; }

        [DisplayName("User Type")]
        public int user_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketManager> TicketManagers { get; set; }
    }
}
