using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework.Legacy;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    internal class Locators
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.
                TextToBePresentInElementValue(driver.FindElement(By.XPath("//input[@value='Sign In']")), "Sign In"));
        }

        [Test]
        public void LocatorsIndentification()
        {
            driver.FindElement(By.Id("username")).SendKeys("Harutyunyan");
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("123456789");
            driver.FindElement(By.XPath("//div[@class = 'form-group'][5]/label/span/input")).Click();
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            string alertText = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(alertText);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            string hrefAttr = link.GetAttribute("href");
            string expectedUrl = "https://rahulshettyacademy.com/documents-request";
            Assert.That(hrefAttr, Is.EqualTo(expectedUrl));
            
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

    }
}
