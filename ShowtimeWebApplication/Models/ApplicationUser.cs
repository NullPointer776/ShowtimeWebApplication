using Microsoft.AspNetCore.Identity;
namespace ShowtimeWebApplication.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
