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
                new Movie {Title = "The Matrix", Genre = Genre.ScienceFiction, Duration = 136  },
                new Movie { Title = "Inception", Genre = Genre.ScienceFiction, Duration = 148 }
            };
        }
        public IActionResult Index()
        {
            var movies = GetMovieList();
            return View(movies);
        }
    }
}
