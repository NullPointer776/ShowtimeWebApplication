namespace ShowtimeWebApplication.Models
{
    public class MovieEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }
    }
}