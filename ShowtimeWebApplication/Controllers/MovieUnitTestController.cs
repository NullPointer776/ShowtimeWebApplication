using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                new Movie {Title = "SF1", Genre = Genre.ScienceFiction, Duration = 136  },
                new Movie { Title = "SF2", Genre = Genre.ScienceFiction, Duration = 148 },
                new Movie { Title = "A", Genre = Genre.Action, Duration = 152 }
            };
        }
        public IActionResult Index(string sortOrder, string genreFilter, string searchString)
        {
            var movies = GetMovieList();
            //Search
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = (List<Movie>)movies.Where(m => m.Title.Contains(searchString));
            }
            //Filter by genre
            if (!string.IsNullOrEmpty(genreFilter))
            {
                var genre = Enum.Parse<Genre>(genreFilter);
                movies = (List<Movie>)movies.Where(m => m.Genre == genre);
            }
            //Sort
            movies = sortOrder == "title_desc" ? (List<Movie>)movies.OrderByDescending(m => m.Title) : (List<Movie>)movies.OrderBy(m => m.Title);
            return View(movies);
        }
        public IActionResult Details(int id)
        {
            var movie = GetMovieList().FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
    }
}
