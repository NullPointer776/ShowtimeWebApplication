using Microsoft.AspNetCore.Mvc;
using ShowtimeWebApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShowtimeTestingProject.Controllers
{
    public class BookingUnitTestController : Controller
    {
        public List<Booking> GetBookingList()
        {
            return new List<Booking>
            {
                new Booking { Id = 1, NumberOfTickets = 2, ShowtimeId = 1, UserId = "user1", BookingDate = new DateTime(2022,1,1) },
                new Booking { Id = 2, NumberOfTickets = 1, ShowtimeId = 2, UserId = "user2", BookingDate = new DateTime(2022,2,2)}
            };
        }

        public IActionResult Index()
        {
            var bookings = GetBookingList();
            return View(bookings);
        }

        public IActionResult Details(int id)
        {
            var booking = GetBookingList().FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }


        public IActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        public IActionResult Delete(int id)
        {
            var booking = GetBookingList().FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}