using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public string? Description { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

        // Helper property to check if event is in progress
        [NotMapped]
        public bool IsOngoing => DateTime.Now >= StartDate && DateTime.Now <= EndDate;

        // Helper property to check if event is upcoming
        [NotMapped]
        public bool IsUpcoming => DateTime.Now < StartDate;

        // Helper property to check if event is completed
        [NotMapped]
        public bool IsCompleted => DateTime.Now > EndDate;
    }
}