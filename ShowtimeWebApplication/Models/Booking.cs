using System.ComponentModel.DataAnnotations;

namespace ShowtimeWebApplication.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; } = DateTime.Now;
        [Required]
        [Range(0,100)]
        public int NumberOfTickets { get; set; }
        public string UserId { get; set; }
        public int ShowtimeId { get; set; }

        public ApplicationUser User { get; set; }
        public Showtime Showtime { get; set; }
    }
}
