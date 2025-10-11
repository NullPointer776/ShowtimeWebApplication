using ShowtimeTestingProject.Controllers;

namespace ShowtimeTestingProject;

[TestClass]
public class MovieListingUnitTest
{
    [TestMethod]
    public void TestSearchMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var movies = controller.GetMovieList();
        var searchString = "Inception";
        var result = movies.Where(m => m.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
        Assert.AreEqual(1, result.Count);
    }
}
