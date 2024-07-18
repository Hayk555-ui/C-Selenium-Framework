using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium
{
    internal class WindowHandler
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void WindowHandlerTest()
        {
            string email = "mentor@rahulshettyacademy.com";

            driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material")).Click();
            var openedWindow = driver.WindowHandles[1];
            driver.SwitchTo().Window(openedWindow);
            TestContext.Progress.WriteLine(driver.FindElement(By.ClassName("red")).Text);

            string textToVerify = driver.FindElement(By.CssSelector(".red")).Text;

            string[] splittedTextToVerify = textToVerify.Split("at");
            string[] trimmedText = splittedTextToVerify[1].Trim().Split(" ");
            Assert.That(email.Equals(trimmedText[0]));

        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
