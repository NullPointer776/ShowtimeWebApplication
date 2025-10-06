using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShowtimeWebApplication.Data;
using ShowtimeWebApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace ShowtimeWebApplication.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoviesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Movies
        [AllowAnonymous]//allow visitor to see the movie list
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Include(m => m.Showtimes)
                .ToListAsync();
            return View(movies);
        }

        // GET: Movies/Details/5
        [AllowAnonymous]//allow visitor to see the movie details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Showtimes) 
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")] // Only Admin can create movies
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Title = viewModel.Title,
                    Genre = viewModel.Genre,
                    Duration = viewModel.Duration
                };

                var showtime = new Showtime
                {
                    StartTime = viewModel.StartTime,
                    AvailableSeats = viewModel.AvailableSeats,
                    Price = viewModel.Price,
                    Movie = movie
                };

                movie.Showtimes.Add(showtime);

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")] // Only Admin can edit movies
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Showtimes) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var viewModel = new MovieEditViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                Duration = movie.Duration,
                StartTime = movie.Showtimes.FirstOrDefault()?.StartTime ?? DateTime.Now.AddDays(1),
                AvailableSeats = movie.Showtimes.FirstOrDefault()?.AvailableSeats ?? 100,
                Price = movie.Showtimes.FirstOrDefault()?.Price ?? 12.50m
            };

            return View(viewModel);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var movie = await _context.Movies
                        .Include(m => m.Showtimes)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (movie == null)
                    {
                        return NotFound();
                    }

                    movie.Title = viewModel.Title;
                    movie.Genre = viewModel.Genre;
                    movie.Duration = viewModel.Duration;

                    var showtime = movie.Showtimes.FirstOrDefault();
                    if (showtime != null)
                    {
                        showtime.StartTime = viewModel.StartTime;
                        showtime.AvailableSeats = viewModel.AvailableSeats;
                        showtime.Price = viewModel.Price;
                    }
                    else
                    {
                        movie.Showtimes.Add(new Showtime
                        {
                            StartTime = viewModel.StartTime,
                            AvailableSeats = viewModel.AvailableSeats,
                            Price = viewModel.Price,
                            MovieId = movie.Id
                        });
                    }

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")] // Only Admin can delete movies
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Showtimes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}