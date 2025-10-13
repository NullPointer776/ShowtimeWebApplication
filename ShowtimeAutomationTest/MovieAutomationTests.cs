using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

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

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
        }
        [TestMethod]
        public void TestCreateMovie()
        {
            // Find search input and enter search term
            var searchInput = _driver.FindElement(By.Name("Create"));
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
        public void TestMovieDetails()
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
        public void TestEditMovie()
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
        public void TestDeleteMovie()
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
    }
}