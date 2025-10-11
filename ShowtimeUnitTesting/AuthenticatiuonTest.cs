using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ShowtimeTestingProject;

[TestClass]
public class AuthenticatiuonTest
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
}
