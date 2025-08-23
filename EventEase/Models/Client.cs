using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Client Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Client name must be between 2 and 100 characters")]
        public string ClientName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}