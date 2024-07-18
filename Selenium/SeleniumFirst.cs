using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium
{
    internal class SeleniumFirst
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        [Test]
        public void First()
        {
            driver.Url = "https://rahulshettyacademy.com/#/index";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
