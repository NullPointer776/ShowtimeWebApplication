using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ShowtimeTestingProject;

[TestClass]
public class MovieListingAutomationTesting
{
    private IWebDriver driver;

    [TestInitialize]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://localhost:7085/Movies");
    }

    [TestCleanup]
    public void Cleanup()
    {
        driver.Quit();
    }

    [TestMethod]
    [Priority(1)]
    public void TestSearchFunctionality()
    {
        var searchBox = driver.FindElement(By.Name("searchString"));
        searchBox.SendKeys("Inception");
        searchBox.Submit();

        var firstMovieTitle = driver.FindElement(By.CssSelector("table tbody tr td:first-child")).Text;
        Assert.AreEqual("Inception", firstMovieTitle);

        var firstMovieTitleXPath = driver.FindElement(By.XPath("//table/tbody/tr[1]/td[1]")).Text;
        Assert.AreEqual("Inception", firstMovieTitleXPath);
        driver.FindElement(By.CssSelector("form[asp-action='Index'] a[asp-action='Index']")).Click();
    }
    [TestMethod]
    [Priority(2)]
    public void TestFilterByGenre()
    {
        var genreDropdown = new SelectElement(driver.FindElement(By.Name("genreFilter")));
        genreDropdown.SelectByText("Action");
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        var movieGenres = driver.FindElements(By.CssSelector("table tbody tr td:nth-child(2)")).Select(e => e.Text).ToList();
        Assert.IsTrue(movieGenres.All(g => g == "Action"));
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        driver.FindElement(By.CssSelector("form:not([asp-action]) a[href='@Url.Action(\"Index\")']")).Click();
    }
    [TestMethod]
    [Priority(3)]
    public void TestSortMovieInAscendingRankByTitle()
    {
        var titleHeader = driver.FindElement(By.CssSelector("th a[href*='sortOrder=title_desc']"));
        titleHeader.Click();
        var movieTitles = driver.FindElements(By.CssSelector("table tbody tr td:first-child")).Select(e => e.Text).ToList();

        var expectedAscendingOrder = new List<string>
        {
            "Forrest Gump",
            "Inception",
            "Pulp Fiction",
            "The Dark Knight",
            "The Matrix",
            "The Shawshank Redemption"
        };

        CollectionAssert.AreEqual(expectedAscendingOrder, movieTitles);
    }

    [TestMethod]
    [Priority(4)]
    public void TestSortMovieInDescendingRankByTitle()
    {
        var titleHeader = driver.FindElement(By.CssSelector("th a[href*='sortOrder=']"));
        titleHeader.Click();
        var movieTitles = driver.FindElements(By.CssSelector("table tbody tr td:first-child")).Select(e => e.Text).ToList();

        var expectedDescendingOrder = new List<string>
        {
            "The Shawshank Redemption",
            "The Matrix",
            "The Dark Knight",
            "Pulp Fiction",
            "Inception",
            "Forrest Gump"
        };

        CollectionAssert.AreEqual(expectedDescendingOrder, movieTitles);
    }
}
