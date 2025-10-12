using Microsoft.AspNetCore.Mvc;
using ShowtimeTestingProject.Controllers;
using ShowtimeWebApplication.Models;
namespace ShowtimeTestingProject;
//Test class for MovieController Advance features (Bruce)
[TestClass]
public class MovieListingUnitTest
{
    //For explicit typecast IActionResult to List<Movie>
    private List<Movie> GetMoviesFromResult(IActionResult result)
    {
        var viewResult = result as ViewResult;
        return (viewResult?.Model as List<Movie>) ?? new List<Movie>();
    }
    //Test methods for Search
    [TestMethod]
    public void TestSearchMethod()
    {
        //Get the controller instance and call the Index method with search string "S"
        var controller = new MoviesUnitTestController();
        var result = controller.Index(null, null, "S");
        var actualMovies = GetMoviesFromResult(result);

        var expectedTitles = new List<string> { "SF1", "SF2" };
        var actualTitles = actualMovies.Select(m => m.Title).ToList();
        //Check if the expected titles match the actual titles
        CollectionAssert.AreEqual(expectedTitles, actualTitles);
    }
    //Test methods for Filter by genre
    [TestMethod]
    public void TestFilterMethod()
    {
        //Get the controller instance and call the Index method with genre "ScienceFiction"
        var controller = new MoviesUnitTestController();
        var result = controller.Index(null, "ScienceFiction", null);
        var actualMovies = GetMoviesFromResult(result);
        //Check if the number of movies returned is 2 and all are of ScienceFiction genre
        Assert.AreEqual(2, actualMovies.Count);
        Assert.IsTrue(actualMovies.All(m => m.Genre == Genre.ScienceFiction));
    }
    //Test methods for Sort Ascending
    [TestMethod]
    public void TestSortAscendingMethod()
    {
        //Get the controller instance and call the Index method with sortOrder as empty string for ascending order
        var controller = new MoviesUnitTestController();
        var result = controller.Index("", null, null);
        var actualMovies = GetMoviesFromResult(result);

        var expectedTitles = new List<string> { "A", "SF1", "SF2" };
        var actualTitles = actualMovies.Select(m => m.Title).ToList();
        //Check if the expected titles match the actual titles in ascending order
        CollectionAssert.AreEqual(expectedTitles, actualTitles);
    }
    //Test methods for Sort Descending
    [TestMethod]
    public void TestSortDescendingMethod()
    {
        //Get the controller instance and call the Index method with sortOrder as "title_desc" for descending order
        var controller = new MoviesUnitTestController();
        var result = controller.Index("title_desc", null, null);
        var actualMovies = GetMoviesFromResult(result);
        
        var expectedTitles = new List<string> { "SF2", "SF1", "A" };
        var actualTitles = actualMovies.Select(m => m.Title).ToList();
        //Check if the expected titles match the actual titles in descending order
        CollectionAssert.AreEqual(expectedTitles, actualTitles);
    }
}