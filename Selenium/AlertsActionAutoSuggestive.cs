using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium
{
    internal class AlertsActionAutoSuggestive
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
        }

        [Test]
        public void Test_Alert()
        {
            string nameInsideText = "Hayk";
            driver.FindElement(By.Id("name")).SendKeys("Hayk");
            driver.FindElement(By.CssSelector("#confirmbtn")).Click();

            string text = driver.SwitchTo().Alert().Text;
            Assert.That(text, Does.Contain(nameInsideText));
        }

        [Test]
        public void Test_Alert_AutoSuggestions()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("Ar");

            IList<IWebElement> options = driver.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach (var item in options)
            {
                if (item.Text.Equals("Armenia"))
                {
                    item.Click();
                }
            }
            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));
        }

        [Test]
        public void MouseMoving()
        {
            driver.Url = "https://rahulshettyacademy.com/";

            Actions action = new(driver);
            action.MoveToElement(driver.FindElement(By.ClassName("dropdown-toggle"))).Perform();
            action.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu'][1]"))).Click().Perform();
            
        }

        [Test]
        public void Frames()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);

            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.LinkText("All Access Plan")).Click();
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
            driver.SwitchTo().DefaultContent();
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
