using ShowtimeTestingProject.Controllers;
using ShowtimeWebApplication.Models;
namespace ShowtimeTestingProject;
//Test class for MovieController Advance features (Bruce)
[TestClass]
public class MovieListingUnitTest
{
    [TestMethod]
    public void TestSearchMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var movies = controller.GetMovieList();
        var searchString = "S";
        var result = controller.Index(searchString, null, null);
        var list=new List<Movie>
        {
            new Movie {Title = "SF1", Genre = Genre.ScienceFiction, Duration = 136  },
            new Movie { Title = "SF2", Genre = Genre.ScienceFiction, Duration = 148 }
        };
        //Checking is the result contains the expected movies
        CollectionAssert.Contains(list, result);
    }
    public void TestFilterMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var movies = controller.GetMovieList();
        var genreFilter = Genre.ScienceFiction;
        var result = controller.Index(null, genreFilter.ToString(), null);

        var list = new List<Movie>
        {
            new Movie {Title = "SF1", Genre = Genre.ScienceFiction, Duration = 136  },
            new Movie { Title = "SF2", Genre = Genre.ScienceFiction, Duration = 148 }
        };
        Assert.IsTrue(result.Any(m => m.Title == "SF1"));
        Assert.IsTrue(actualMovies.Any(m => m.Title == "SF2"));
    }
    public void TestSortAscendingMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var movies = controller.GetMovieList();
        var sortOrder = "";
        var result = controller.Index(sortOrder,null,null);
        var list = new List<Movie>
        {
            new Movie { Title = "A", Genre = Genre.Action, Duration = 152 },
            new Movie {Title = "SF1", Genre = Genre.ScienceFiction, Duration = 136  },
            new Movie { Title = "SF2", Genre = Genre.ScienceFiction, Duration = 148 }
        };
        for (int i=0;i<list.Count;i++)
        {
            Assert.IsTrue(list[i].Title, result[i].Title);
        }
        
    }
    public void TestSortDescendingMethod()
    {
        MoviesUnitTestController controller = new MoviesUnitTestController();
        var movies = controller.GetMovieList();
        var searchString = "Inception";
        var result = movies.Where(m => m.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
        Assert.AreEqual(1, result.Count);
    }
}
