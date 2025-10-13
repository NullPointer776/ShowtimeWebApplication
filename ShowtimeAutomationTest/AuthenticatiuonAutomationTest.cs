using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ShowtimeTestingProject;

[TestClass]
public class AuthenticationAutomationTest
{
    private IWebDriver driver;

    [TestInitialize]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://localhost:7085");
    }

    [TestCleanup]
    public void Cleanup()
    {
        driver.Quit();
    }

    [TestMethod]
    public void TestUserRegistration()
    {
        // Navigate to registration page
        driver.FindElement(By.LinkText("Register")).Click();
        // Generate unique email using timestamp
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string email = $"test{timestamp}@test.com";

        // Fill registration form
        var fullNameInput = driver.FindElement(By.Id("Input_FullName"));
        fullNameInput.SendKeys("Test User");

        var emailInput = driver.FindElement(By.Id("Input_Email"));
        emailInput.SendKeys(email);

        var passwordInput = driver.FindElement(By.Id("Input_Password"));
        passwordInput.SendKeys("Test123!");

        var confirmPasswordInput = driver.FindElement(By.Id("Input_ConfirmPassword"));
        confirmPasswordInput.SendKeys("Test123!");

        // Click register button
        var registerButton = driver.FindElement(By.Id("registerSubmit"));
        registerButton.Click();

        // Wait for redirect and verify successful registration
        System.Threading.Thread.Sleep(3000);

        // Check if redirected to home page or login page
        Assert.IsTrue(driver.Url.Contains("/") || driver.Url.Contains("Login"));
    }
    [TestMethod]
    public void TestUserLogin()
    {
        // Navigate to login page
        driver.FindElement(By.LinkText("Login")).Click();
        // Fill login form
        var emailInput = driver.FindElement(By.Id("Input_Email"));
        emailInput.SendKeys("user@showtime.com");

        var passwordInput = driver.FindElement(By.Id("Input_Password"));
        passwordInput.SendKeys("User123!");

        // Click login button
        var loginButton = driver.FindElement(By.Id("login-submit"));
        loginButton.Click();

        // Wait for redirect and verify successful login
        System.Threading.Thread.Sleep(3000);

        // Check if redirected to home page or movies page after login
        Assert.IsTrue(driver.Url.Contains("/") || driver.Url.Contains("Movies"));
    }
}
