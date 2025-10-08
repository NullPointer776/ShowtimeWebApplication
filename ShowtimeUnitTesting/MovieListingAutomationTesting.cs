using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
    public void TestSearchFunctionality()
    {
        var searchBox = driver.FindElement(By.Name("searchString"));
        searchBox.SendKeys("Inception");
        searchBox.Submit();

        System.Threading.Thread.Sleep(2000);

        var firstMovieTitle = driver.FindElement(By.CssSelector("table tbody tr td:first-child")).Text;
        Assert.AreEqual("Inception", firstMovieTitle);

        var firstMovieTitleXPath = driver.FindElement(By.XPath("//table/tbody/tr[1]/td[1]")).Text;
        Assert.AreEqual("Inception", firstMovieTitleXPath);
    }
}
