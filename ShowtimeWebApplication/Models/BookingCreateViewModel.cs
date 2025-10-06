using System.ComponentModel.DataAnnotations;

namespace ShowtimeWebApplication.Models
{
    public class BookingCreateViewModel
    {
        [Required]
        [Range(1, 10, ErrorMessage = "Number of tickets must be between 1 and 10")]
        public int NumberOfTickets { get; set; }

        [Required]
        public int ShowtimeId { get; set; }
    }
}
