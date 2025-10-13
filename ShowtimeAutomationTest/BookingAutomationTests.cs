using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ShowtimeTestingProject;

[TestClass]
public class BookingAutomationTesting
{
    private IWebDriver driver;

    [TestInitialize]
    public void Setup()
    {
        driver = new ChromeDriver();
        //driver.Navigate().GoToUrl("https://localhost:7085/Bookings");
    }

    private void Login()
    {
        driver.Navigate().GoToUrl("https://localhost:7085/Identity/Account/Login");
        driver.FindElement(By.Id("Input_Email")).SendKeys("admin@showtime.com");
        driver.FindElement(By.Id("Input_Password")).SendKeys("Admin123!");
        driver.FindElement(By.Id("login-submit")).Click();
        Thread.Sleep(1500);
    }

    [TestCleanup]
    public void Cleanup()
    {
        driver.Quit();
    }

    [TestMethod]
    [Priority(1)]
    public void TestCreateBooking()
    {
        Login();

        driver.Navigate().GoToUrl("https://localhost:7085/Bookings/Create");

        var ticketInput = driver.FindElement(By.Id("NumberOfTickets"));
        ticketInput.Clear();
        ticketInput.SendKeys("2");

        var showtimeDropdown = driver.FindElement(By.Id("ShowtimeId"));
        showtimeDropdown.Click();
        var options = showtimeDropdown.FindElements(By.TagName("option"));

        driver.FindElement(By.CssSelector("input[type='submit'][value='Create']")).Click();

        Thread.Sleep(2000);
        Assert.IsTrue(driver.Url.Contains("/Bookings"));
        Assert.IsTrue(driver.PageSource.Contains("Bookings"));
    }

    [TestMethod]
    [Priority(2)]
    public void TestDetailsBooking()
    {
        Login();

        driver.Navigate().GoToUrl("https://localhost:7085/Bookings");

        var detailsLinks = driver.FindElements(By.LinkText("Details"));
        Assert.IsTrue(detailsLinks.Count > 0);
        detailsLinks[0].Click();

        Thread.Sleep(1500);
        Assert.IsTrue(driver.Url.Contains("/Bookings/Details"));
        Assert.IsTrue(driver.PageSource.Contains("NumberOfTickets"));
        Assert.IsTrue(driver.PageSource.Contains("Showtime"));
    }

    [TestMethod]
    [Priority(3)]
    public void TestDeleteBooking()
    {
        Login();

        driver.Navigate().GoToUrl("https://localhost:7085/Bookings");

        var deleteLink = driver.FindElement(By.CssSelector("table tbody tr td a[href*='Delete']"));
        deleteLink.Click();
        Thread.Sleep(500);

        var confirmButton = driver.FindElement(By.CssSelector("input[type='submit'][value='Delete']"));
        confirmButton.Click();
        Thread.Sleep(1000);

    }
}
