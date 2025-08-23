using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number")]
        public int Capacity { get; set; }

        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [NotMapped] 
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}