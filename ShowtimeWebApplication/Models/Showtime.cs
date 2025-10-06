namespace ShowtimeWebApplication.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Price { get; set; }
        // Foreign Keys
        public int MovieId { get; set; }

        // Navigation Properties
        public Movie Movie { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
