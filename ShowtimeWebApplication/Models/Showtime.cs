using System.ComponentModel.DataAnnotations;

namespace ShowtimeWebApplication.Models
{
    public class Showtime
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public decimal Price { get; set; }
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
