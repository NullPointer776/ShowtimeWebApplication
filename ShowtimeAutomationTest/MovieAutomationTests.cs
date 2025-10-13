using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace ShowtimeWebApplication.AutomationTests
{
    [TestClass]
    public class MovieAutomationTests
    {
        private readonly IWebDriver _driver;

        public MovieAutomationTests()
        {
            _driver = new ChromeDriver();
        }

        [TestInitialize]
        public void LaunchBrowser()
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://localhost:7085/Movies");
        }
        private void Login()
        {
            _driver.Navigate().GoToUrl("https://localhost:7085/Identity/Account/Login");
            _driver.FindElement(By.Id("Input_Email")).SendKeys("admin@showtime.com");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Admin123!");
            _driver.FindElement(By.Id("login-submit")).Click();
            _driver.FindElement(By.LinkText("Movie")).Click();
            Thread.Sleep(1500);
        }
        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void TestCreateMovie()
        {
            Login();
            _driver.FindElement(By.LinkText("Create New")).Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.Id("Title")));

            _driver.FindElement(By.Id("Title")).SendKeys("Test Movie");
            _driver.FindElement(By.Id("Duration")).SendKeys("120");

            new SelectElement(_driver.FindElement(By.Id("Genre"))).SelectByText("Action");
            _driver.FindElement(By.Id("StartTime")).SendKeys("2024-01-01T20:00");
            _driver.FindElement(By.Id("Price")).SendKeys("12.99");

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.TagName("tbody")));
            Assert.IsTrue(_driver.FindElement(By.TagName("tbody")).Text.Contains("Test Movie"));
        }

        [TestMethod]
        public void TestMovieDetails()
        {
            Login();
            _driver.FindElements(By.LinkText("Details"))[0].Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.TagName("h2")));

            Assert.IsNotNull(_driver.FindElement(By.TagName("h2")).Text);
            Assert.IsNotNull(_driver.FindElement(By.TagName("dl")).Text);
        }

        [TestMethod]
        public void TestEditMovie()
        {
            Login();
            _driver.FindElements(By.LinkText("Edit"))[0].Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.Id("Title")));

            _driver.FindElement(By.Id("Title")).Clear();
            _driver.FindElement(By.Id("Title")).SendKeys("Updated Movie Title");
            _driver.FindElement(By.Id("Duration")).Clear();
            _driver.FindElement(By.Id("Duration")).SendKeys("130");

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.TagName("tbody")));
            Assert.IsTrue(_driver.FindElement(By.TagName("tbody")).Text.Contains("Updated Movie Title"));
        }

        [TestMethod]
        public void TestDeleteMovie()
        {
            Login();
            _driver.FindElements(By.LinkText("Delete"))[0].Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.TagName("h3")));

            _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.TagName("tbody")));

            var movieTitles = _driver.FindElements(By.TagName("td")).Where(td => td.Text == "Test Movie");
            Assert.AreEqual(0, movieTitles.Count());
        }
    }
}