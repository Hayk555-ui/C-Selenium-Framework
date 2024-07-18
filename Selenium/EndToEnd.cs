using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebDriverManager.DriverConfigs.Impl;
using AngleSharp.Text;
using NUnit.Framework.Legacy;

namespace CSharpFundamnetals
{
    internal class EndToEnd
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
        public void EndToEndFlow()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//div[@class = 'form-group'][5]/label/span/input")).Click();
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.PartialLinkText("Checkout")));

            string[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualResult = new string[2];

            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));
            foreach (IWebElement product in products)
            {
                if(expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
                TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);
                Thread.Sleep(7000);
            }
            driver.FindElement(By.PartialLinkText("Checkout")).Click();
            IList<IWebElement> itemsInCard = driver.FindElements(By.CssSelector("h4 a"));

            for(int i = 0; i < itemsInCard.Count; i++)
            {
                actualResult[i] = itemsInCard[i].Text;
            }
            Assert.That(actualResult, Is.EqualTo(expectedProducts));

            driver.FindElement(By.XPath("//button[@class='btn btn-success']")).Click();
            driver.FindElement(By.Id("country")).SendKeys("Arm");

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Armenia")));
            driver.FindElement(By.LinkText("Armenia")).Click();
            driver.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            driver.FindElement(By.XPath("//input[@value='Purchase']")).Click();
            string successText = driver.FindElement(By.ClassName("alert-dismissible")).Text;

            Assert.That(successText, Does.Contain("Thank you!"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

    }
}
