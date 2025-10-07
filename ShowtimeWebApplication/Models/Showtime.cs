using System.ComponentModel.DataAnnotations;

namespace ShowtimeWebApplication.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]

        public decimal Price { get; set; }
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
