using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium
{
    internal class FunctionalTest
    {

        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
            
        }

        [Test]
        public void dropdown()
        {
            IWebElement dropdown = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement selectElement = new SelectElement(dropdown);
            selectElement.SelectByText("Teacher");
            selectElement.SelectByValue("consult");

            IWebElement userRadioButton = driver.FindElement(By.XPath("//input[@value='user']"));
            userRadioButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));

            IWebElement buttonOk = driver.FindElement(By.Id("okayBtn"));
            buttonOk.Click();
            bool result = driver.FindElement(By.XPath("//input[@checked='checked']")).Selected;
            Assert.That(result, Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
