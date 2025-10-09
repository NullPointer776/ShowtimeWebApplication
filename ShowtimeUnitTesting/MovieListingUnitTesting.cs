using Microsoft.AspNetCore.Mvc;
using ShowtimeTestingProject.Controllers;
using System.Linq;

namespace ShowtimeTestingProject.Tests
{
    [TestClass]
    public class MoviesUnitTest
    {
        [TestMethod]
        public void TestIndex()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();

            // Act
            IActionResult result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMovies()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();

            // Act
            IActionResult result = controller.Movies();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetails_ValidId()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();
            int validId = 1;

            // Act
            IActionResult result = controller.Details(validId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetails_InvalidId()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();
            int invalidId = 999;

            // Act
            IActionResult result = controller.Details(invalidId);

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void TestSearch_WithResults()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();
            string searchTerm = "Avengers";

            // Act
            IActionResult result = controller.Search(searchTerm);


        }

        [TestMethod]
        public void TestSearch_EmptyString()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();
            string emptySearch = "";

            // Act
            IActionResult result = controller.Search(emptySearch);

        }

        [TestMethod]
        public void TestFilterByGenre_ActionGenre()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();
            string genre = "Action";

            // Act
            IActionResult result = controller.FilterByGenre(genre);

            // Assert
        }

        [TestMethod]
        public void TestFilterByGenre_EmptyGenre()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();
            string emptyGenre = "";

            // Act
            IActionResult result = controller.FilterByGenre(emptyGenre);

        }

        [TestMethod]
        public void TestGetMovieList()
        {
            // Arrange
            MoviesUnitTestController controller = new MoviesUnitTestController();

            // Act
            var movieList = controller.GetMovieList();

            // Assert
            Assert.IsNotNull(movieList);

        }
    }
}