using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DatesAndStuff.Web.Tests;

[TestFixture]
public class FlightSelectionTests
{
    private IWebDriver driver;
    private const string BaseUrl = "https://blazedemo.com";

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
    }

    [TearDown]
    public void Teardown()
    {
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
        }
    }

    [Test]
    public void FlightsBetweenMexicoCityAndDublin_ShouldBeAtLeastThree()
    {
        driver.Navigate().GoToUrl(BaseUrl);
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        driver.FindElement(By.Name("fromPort")).SendKeys("Mexico City");

        driver.FindElement(By.Name("toPort")).SendKeys("Dublin");

        driver.FindElement(By.CssSelector("input[type='submit']")).Click();

        By tableRowsLocator = By.CssSelector("table.table tbody tr");
        wait.Until(d => d.FindElements(tableRowsLocator).Count > 0);

        var flightRows = driver.FindElements(tableRowsLocator);

        flightRows.Count.Should().BeGreaterThanOrEqualTo(3, "There should be at least 3 flights from Mexico City to Dublin.");
    }
}

