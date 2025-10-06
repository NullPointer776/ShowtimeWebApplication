namespace ShowtimeWebApplication.Models
{
    public class MovieCreateViewModel
    {
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int Duration { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now.AddDays(1);
        public int AvailableSeats { get; set; } = 100;
        public decimal Price { get; set; } = 12.50m;
    }
}
