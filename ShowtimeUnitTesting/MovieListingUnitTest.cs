using Microsoft.AspNetCore.Mvc;
using ShowtimeTestingProject.Controllers;
using ShowtimeWebApplication.Models;
namespace ShowtimeTestingProject;
//Test class for MovieController Advance features (Bruce)
[TestClass]
public class MovieListingUnitTest
{
    private List<Movie> GetMoviesFromResult(IActionResult result)
    {
        var viewResult = result as ViewResult;
        return (viewResult?.Model as List<Movie>) ?? new List<Movie>();
    }

    [TestMethod]
    public void TestSearchMethod()
    {
        var controller = new MoviesUnitTestController();
        var result = controller.Index(null, null, "S");
        var actualMovies = GetMoviesFromResult(result);

        var expectedTitles = new List<string> { "SF1", "SF2" };
        var actualTitles = actualMovies.Select(m => m.Title).ToList();

        CollectionAssert.AreEqual(expectedTitles, actualTitles);
    }

    [TestMethod]
    public void TestFilterMethod()
    {
        var controller = new MoviesUnitTestController();
        var result = controller.Index(null, "ScienceFiction", null);
        var actualMovies = GetMoviesFromResult(result);

        Assert.AreEqual(2, actualMovies.Count);
        Assert.IsTrue(actualMovies.All(m => m.Genre == Genre.ScienceFiction));
    }

    [TestMethod]
    public void TestSortAscendingMethod()
    {
        var controller = new MoviesUnitTestController();
        var result = controller.Index("", null, null);
        var actualMovies = GetMoviesFromResult(result);

        var expectedTitles = new List<string> { "A", "SF1", "SF2" };
        var actualTitles = actualMovies.Select(m => m.Title).ToList();

        CollectionAssert.AreEqual(expectedTitles, actualTitles);
    }

    [TestMethod]
    public void TestSortDescendingMethod()
    {
        var controller = new MoviesUnitTestController();
        var result = controller.Index("title_desc", null, null);
        var actualMovies = GetMoviesFromResult(result);

        var expectedTitles = new List<string> { "SF2", "SF1", "A" };
        var actualTitles = actualMovies.Select(m => m.Title).ToList();

        CollectionAssert.AreEqual(expectedTitles, actualTitles);
    }
}