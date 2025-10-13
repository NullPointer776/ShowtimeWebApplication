using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ShowtimeTestingProject;

[TestClass]
public class MovieListingAutomationTest
{
    private readonly IWebDriver _driver;

    public MovieListingAutomationTest()
    {
        _driver = new ChromeDriver();
    }

    [TestInitialize]
    public void LaunchBrowser()
    {
        _driver.Manage().Window.Maximize();
        _driver.Navigate().GoToUrl("https://localhost:7085/Movies");
    }

    [TestCleanup]
    public void Cleanup()
    {
        _driver.Quit();
    }

    [TestMethod]
    public void TestSearchFunctionality()
    {
        // Find search input and enter search term
        var searchInput = _driver.FindElement(By.Name("searchString"));
        searchInput.SendKeys("Matrix");

        // Click search button
        var buttons = _driver.FindElements(By.TagName("button"));
        var searchButton = buttons.First(b => b.Text == "Search");
        searchButton.Click();

        // Wait for results and verify
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(By.TagName("tbody")));

        // Check if search results contain expected movie
        var tableBody = _driver.FindElement(By.TagName("tbody"));
        Assert.IsTrue(tableBody.Text.Contains("The Matrix"));
    }

    [TestMethod]
    public void TestFilterFunctionality()
    {
        // Find genre filter dropdown
        var genreFilter = _driver.FindElement(By.Name("genreFilter"));
        var selectElement = new SelectElement(genreFilter);

        // Select Drama genre
        selectElement.SelectByText("Drama");

        // Click filter button
        var buttons = _driver.FindElements(By.TagName("button"));
        var filterButton = buttons.First(b => b.Text == "Filter");
        filterButton.Click();

        // Wait for results and verify
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(By.TagName("tbody")));

        // Check if filtered results contain drama movies
        var tableBody = _driver.FindElement(By.TagName("tbody"));
        Assert.IsTrue(tableBody.Text.Contains("Forrest Gump"));
        Assert.IsTrue(tableBody.Text.Contains("The Shawshank Redemption"));
    }

    [TestMethod]
    public void TestSortAscending()
    {
        // Click A-Z sort button
        var aTozButton = _driver.FindElement(By.LinkText("A-Z"));
        aTozButton.Click();

        // Wait for results
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(By.TagName("tbody")));

        // Get all movie titles from the table
        var titles = _driver.FindElements(By.XPath("//tbody/tr/td[1]"))
            .Where(td => !string.IsNullOrEmpty(td.Text)).Select(t => t.Text).ToList();

        // Verify titles are in ascending order
        var sortedTitles = titles.OrderBy(t => t).ToList();
        CollectionAssert.AreEqual(sortedTitles, titles);
    }

    [TestMethod]
    public void TestSortDescending()
    {
        // Click Z-A sort button
        var aTozButton = _driver.FindElement(By.LinkText("Z-A"));
        aTozButton.Click();

        // Wait for results
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(By.TagName("tbody")));

        // Get all movie titles from the table
        var titles = _driver.FindElements(By.XPath("//tbody/tr/td[1]"))
            .Where(td => !string.IsNullOrEmpty(td.Text)).Select(t => t.Text).ToList();

        // Verify titles are in descending order
        var sortedTitles = titles.OrderByDescending(t => t).ToList();
        CollectionAssert.AreEqual(sortedTitles, titles);
    }

    [TestMethod]
    public void TestResetSearch()
    {
        // Perform a search first
        var searchInput = _driver.FindElement(By.Name("searchString"));
        searchInput.SendKeys("Matrix");

        var searchButton = _driver.FindElements(By.TagName("button")).First(b => b.Text == "Search");
        searchButton.Click();

        // Click reset button
        var resetButtons = _driver.FindElements(By.TagName("a")).Where(l => l.Text == "Reset").ToList();
        var searchResetButton = resetButtons[0]; // First reset button is for search
        searchResetButton.Click();

        // Wait for results and verify all movies are displayed
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(By.TagName("tbody")));

        var tableBody = _driver.FindElement(By.TagName("tbody"));
        var movieRows = _driver.FindElements(By.TagName("tr"));

        // Should display more than just the searched movie
        Assert.IsTrue(movieRows.Count > 1);
    }

    [TestMethod]
    public void TestResetFilter()
    {
        // Apply a filter first
        var genreFilter = _driver.FindElement(By.Name("genreFilter"));
        var selectElement = new SelectElement(genreFilter);
        selectElement.SelectByText("Drama");

        var filterButton = _driver.FindElements(By.TagName("button")).First(b => b.Text == "Filter");
        filterButton.Click();

        // Wait for filtered results
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.FindElement(By.TagName("tbody")));

        // Click filter reset button
        var resetButtons = _driver.FindElements(By.TagName("a")).Where(l => l.Text == "Reset").ToList();
        var searchResetButton = resetButtons[0]; // Second reset button is for search
        searchResetButton.Click();


        // Wait for all results to load
        wait.Until(d => d.FindElement(By.TagName("tbody")));

        // Verify all movies are displayed (not just drama)
        var tableBody = _driver.FindElement(By.TagName("tbody"));
        var movieRows = _driver.FindElements(By.TagName("tr"));

        // Should display movies from multiple genres after reset
        Assert.IsTrue(movieRows.Count > 3);
        Assert.IsTrue(tableBody.Text.Contains("The Matrix"));
        Assert.IsTrue(tableBody.Text.Contains("Forrest Gump"));
        Assert.IsTrue(tableBody.Text.Contains("The Dark Knight"));
    }
}