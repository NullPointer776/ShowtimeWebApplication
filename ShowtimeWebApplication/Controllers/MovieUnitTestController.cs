using Microsoft.AspNetCore.Mvc;
using ShowtimeWebApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShowtimeTestingProject.Controllers
{
    public class MoviesUnitTestController : Controller
    {
        public List<Movie> GetMovieList()
        {
            return new List<Movie>
            {
                new Movie { Title = "SF1", Genre = Genre.ScienceFiction, Duration = 136 },
                new Movie { Title = "SF2", Genre = Genre.ScienceFiction, Duration = 148 },
                new Movie { Title = "A", Genre = Genre.Action, Duration = 152 }
            };
        }

        public IActionResult Index(string sortOrder, string genreFilter, string searchString)
        {
            IEnumerable<Movie> movies = GetMovieList();

            // Search with case-insensitive comparison
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m =>
                    m.Title.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));
            }

            // Filter by genre with error handling
            if (!string.IsNullOrEmpty(genreFilter))
            {
                if (System.Enum.TryParse<Genre>(genreFilter, out var genre))
                {
                    movies = movies.Where(m => m.Genre == genre);
                }
            }

            // Sort
            movies = sortOrder == "title_desc"
                ? movies.OrderByDescending(m => m.Title)
                : movies.OrderBy(m => m.Title);

            return View(movies.ToList());
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}