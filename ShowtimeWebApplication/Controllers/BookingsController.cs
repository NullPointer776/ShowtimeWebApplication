using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using ShowtimeWebApplication.Data;
using ShowtimeWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowtimeWebApplication.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        [Authorize]// Only authenticated users can access bookings
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<Booking> bookings;
            if (User.IsInRole("Admin"))
            {
                bookings = await _context.Bookings
                    .Include(b => b.Showtime)
                        .ThenInclude(s => s.Movie)
                    .Include(b => b.User)
                    .ToListAsync();
            }
            else
            {
                bookings = await _context.Bookings
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Showtime)
                        .ThenInclude(s => s.Movie)
                    .Include(b => b.User)
                    .ToListAsync();
            }
            return View(bookings);

        }

        // GET: Bookings/Details/5
        [Authorize]// Only authenticated users can access bookings
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        [Authorize(Roles = "User,Admin")]// Only authenticated users can create booking
        public IActionResult Create()
        {
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes
                .Include(s => s.Movie)
                .Select(s => new {
                        s.Id,
                        DisplayText = $"{s.Movie.Title} - {s.StartTime:MMM dd, yyyy h:mm tt} - ${s.Price}"
                }), "Id", "DisplayText");

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var booking = new Booking
                {
                    NumberOfTickets = viewModel.NumberOfTickets,
                    ShowtimeId = viewModel.ShowtimeId,
                    UserId = _userManager.GetUserId(User), 
                    BookingDate = DateTime.Now 
                };

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes
                .Include(s => s.Movie)
                .Select(s => new {
                    s.Id,
                    DisplayText = $"{s.Movie.Title} - {s.StartTime:MMM dd, yyyy h:mm tt} - ${s.Price}"
                }), "Id", "DisplayText", viewModel.ShowtimeId);

            return View(viewModel);
        }

        // GET: Bookings/Edit/5
        [Authorize(Roles = "User,Admin")]// Only authenticated users can edit booking
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            var viewModel = new BookingEditViewModel
            {
                Id = booking.Id,
                NumberOfTickets = booking.NumberOfTickets,
                ShowtimeId = booking.ShowtimeId,
                UserFullName = booking.User.FullName,
                BookingDate = booking.BookingDate,
                MovieTitle = booking.Showtime.Movie.Title,
                Showtime = booking.Showtime.StartTime
            };
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes
                .Include(s => s.Movie)
                .Select(s => new {
                    s.Id,
                    DisplayText = $"{s.Movie.Title} - {s.StartTime:MMM dd, yyyy h:mm tt}"
                }), "Id", "DisplayText", booking.ShowtimeId);

            return View(viewModel);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookingEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                    var booking = await _context.Bookings.FindAsync(id);
                    if (booking == null)
                    {
                        return NotFound();
                    }

                    booking.NumberOfTickets = viewModel.NumberOfTickets;
                    booking.ShowtimeId = viewModel.ShowtimeId;

                    _context.Update(booking);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var bookingForDisplay = await _context.Bookings
                .Include(b => b.Showtime.Movie)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookingForDisplay != null)
            {
                viewModel.UserFullName = bookingForDisplay.User.FullName;
                viewModel.BookingDate = bookingForDisplay.BookingDate;
                viewModel.MovieTitle = bookingForDisplay.Showtime.Movie.Title;
                viewModel.Showtime = bookingForDisplay.Showtime.StartTime;
            }

            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes
                .Include(s => s.Movie)
                .Select(s => new {
                    s.Id,
                    DisplayText = $"{s.Movie.Title} - {s.StartTime:MMM dd, yyyy h:mm tt}"
                }), "Id", "DisplayText", viewModel.ShowtimeId);

            return View(viewModel);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "User,Admin")]// Only authenticated users can delete booking
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Showtime)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
