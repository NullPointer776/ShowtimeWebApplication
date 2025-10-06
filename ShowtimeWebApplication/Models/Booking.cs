namespace ShowtimeWebApplication.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public int NumberOfTickets { get; set; }
        public string UserId { get; set; }
        public int ShowtimeId { get; set; }

        public ApplicationUser User { get; set; }
        public Showtime Showtime { get; set; }
    }
}
