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
        var test = controller.GetMovieList();
        var id = test[0].Id;
        var result = controller.Details(id);
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
        var test= controller.GetMovieList();
        var id= test[0].Id;
        var result = controller.Edit(id, "Updated Movie Title", Genre.Drama, 180);
        var movie = (result as ViewResult)?.Model as Movie;

        Assert.IsNotNull(movie);
        Assert.AreEqual("Updated Movie Title", movie.Title);
        Assert.AreEqual(Genre.Drama, movie.Genre);
        Assert.AreEqual(180, movie.Duration);
    }
}

