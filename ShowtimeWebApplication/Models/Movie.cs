namespace ShowtimeWebApplication.Models
{
    public enum Genre
    {
        Action,
        Comedy,
        Drama,
        Horror,
        ScienceFiction,
        Romance,
        Thriller,
        Documentary,
        Animation,
        Fantasy
    }
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; } = Genre.Action;
        public int Duration { get; set; } 
        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
        public ICollection<Booking> Bookings { get; set; }
    }
}
