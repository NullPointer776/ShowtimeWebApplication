using Microsoft.AspNetCore.Mvc;
using ShowtimeWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowtimeTestingProject.Controllers
{
    public class BookingUnitTestController : Controller
    {
        //just a fake list of bookings for testing
        public List<Booking> GetBookingList()
        {
            return new List<Booking>
            {
                new Booking
                {
                    Id = 1,
                    NumberOfTickets = 2,
                    ShowtimeId = 1,
                    UserId = "user1",
                    BookingDate = new DateTime(2022,1,1),
                    User = new ApplicationUser { FullName = "John Doe" },
                    Showtime = new Showtime
                    {
                        StartTime = new DateTime(2022,1,5,18,30,0),
                        Price = 12.99m,
                        Movie = new Movie
                        {
                            Title = "Inception",
                            Genre = Genre.ScienceFiction,
                            Duration = 148
                        }
                    }
                },
                new Booking
                {
                    Id = 2,
                    NumberOfTickets = 1,
                    ShowtimeId = 2,
                    UserId = "user2",
                    BookingDate = new DateTime(2022,2,2),
                    User = new ApplicationUser { FullName = "Jane Smith" },
                    Showtime = new Showtime
                    {
                        StartTime = new DateTime(2022,2,10,20,0,0),
                        Price = 14.50m,
                        Movie = new Movie
                        {
                            Title = "The Dark Knight",
                            Genre = Genre.Action,
                            Duration = 152
                        }
                    }
                }
            };
        }

        //show all bookings
        public IActionResult Index()
        {
            var bookings = GetBookingList();
            return View(bookings);
        }

        //show details for 1 booking
        public IActionResult Details(int id)
        {
            var booking = GetBookingList().FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        //create a booking
        public IActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        //show delete confirmation
        public IActionResult Delete(int id)
        {
            var booking = GetBookingList().FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        //delete and go back to index
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
