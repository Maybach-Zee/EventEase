using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [Display(Name = "Event")]
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Booking Date")]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [ForeignKey("EventId")]
        public virtual Event? Event { get; set; }

        [ForeignKey("VenueId")]
        public virtual Venue? Venue { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; }
    }
}