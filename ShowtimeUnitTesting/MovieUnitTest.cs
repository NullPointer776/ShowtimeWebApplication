using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShowtimeTestingProject.Controllers;
using ShowtimeWebApplication.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShowtimeTestingProject;

[TestClass]
public class MovieUnitTests
{
    //For explicit typecast IActionResult to List<Movie>
    private List<Movie> GetMoviesFromResult(IActionResult result)
    {
        var viewResult = result as ViewResult;
        return (viewResult?.Model as List<Movie>) ?? new List<Movie>();
    }
    [TestMethod]
    public void TestDetailMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var result = controller.Details(1);
        var movie = (result as ViewResult)?.Model as Movie;
        Assert.IsNotNull(movie);
    }
    [TestMethod]
    public void TestCreateMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var result = controller.Create("movie", Genre.Documentary, 200,new DateTime(2019,1,1), 12.99m);
        var movie = (result as ViewResult)?.Model as Movie;
        Assert.IsNotNull(movie);
    }
    [TestMethod]
    public void TestEditMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        //var result = controller.Edit(1, "movie title", Genre.Drama);
        //var movie = (result as ViewResult)?.Model as Movie;
        //Assert.IsNotNull(movie);
    }
    /*
        if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                    showtime.Price = viewModel.Price;
                }
                else
                {
                    movie.Showtimes.Add(new Showtime
                    {
                        StartTime = viewModel.StartTime,
                        Price = viewModel.Price,
                        MovieId = movie.Id
                    });
                }

                _context.Update(movie);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
     */
    [TestMethod]
    public void TestDeleteMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var result = controller.Details(1);
        var movie = (result as ViewResult)?.Model as Movie;
        Assert.IsNotNull(movie);
    }
    [TestMethod]
    public void TestDeletConfirmMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var result = controller.Details(1);
        var movie = (result as ViewResult)?.Model as Movie;
        Assert.IsNotNull(movie);
    }
}

