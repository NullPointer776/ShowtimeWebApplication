using System.ComponentModel.DataAnnotations;

namespace ShowtimeWebApplication.Models
{
    public class BookingEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Number of tickets must be between 1 and 10")]
        public int NumberOfTickets { get; set; }

        [Required]
        public int ShowtimeId { get; set; }

        public string UserFullName { get; set; }
        public DateTime BookingDate { get; set; }
        public string MovieTitle { get; set; }
        public DateTime Showtime { get; set; }
    }
}