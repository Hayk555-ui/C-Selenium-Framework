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
using C_SeleniumFramework.utilities;
using C_SeleniumFramework.pageObjects;

namespace C_SeleniumFramework.tests
{
    [Parallelizable(ParallelScope.Self)]
    internal class End2End : Base
    {
        //[Parallelizable(ParallelScope.All)]
        [Test, TestCaseSource(nameof(AddTestDataConfig)), Category("Regression")]
        //[TestCase("rahulshettyacademy", "learning")]
        public void EndToEndFlow(string username, string psw, string[] expectedProducts)
        {
            LoginPage loginPage = new (Driver);
            ProductsPage productsPage = loginPage.ValidLogin(username, psw);
            productsPage.WaitForPageToDisplay(16);

            string[] actualResult = new string[2];

            IList<IWebElement> products = productsPage.GetCards();
            foreach (IWebElement product in products)
            {
                if (expectedProducts.Contains(product.FindElement(productsPage.GetCardTitle()).Text))
                {
                    product.FindElement(productsPage.AddToCardButton()).Click();
                }
                TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);
                Thread.Sleep(7000);
            }
            CheckoutPage checkoutPage = productsPage.CheckoutButton();
            IList<IWebElement> itemsInCard = checkoutPage.GetCheckoutCards();

            for (int i = 0; i < itemsInCard.Count; i++)
            {
                actualResult[i] = itemsInCard[i].Text;
            }
            Assert.That(actualResult, Is.EqualTo(expectedProducts));
            checkoutPage.Checkout();

            driver.Value.FindElement(By.Id("country")).SendKeys("Arm");

            // wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Armenia")));
            driver.Value.FindElement(By.LinkText("Armenia")).Click();
            driver.Value.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            driver.Value.FindElement(By.XPath("//input[@value='Purchase']")).Click();
            string successText = driver.Value.FindElement(By.ClassName("alert-dismissible")).Text;

            Assert.That(successText, Does.Contain("Thank you!"));
        }
        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(GetDataParser().ExtractData("username"), GetDataParser().ExtractData("password"),
                GetDataParser().ExtractDataArray("products"));
            yield return new TestCaseData(GetDataParser().ExtractData("username"), GetDataParser().ExtractData("password"),
                GetDataParser().ExtractDataArray("products"));
            yield return new TestCaseData(GetDataParser().ExtractData("wrong_username"),
                GetDataParser().ExtractData("wrong_password"), GetDataParser().ExtractDataArray("products"));
        }

        [Test, Category("Smoke")]
        public void LocatorsIndentification()
        {
            driver.Value.FindElement(By.Id("username")).SendKeys("Harutyunyan");
            driver.Value.FindElement(By.Id("password")).Clear();
            driver.Value.FindElement(By.Id("password")).SendKeys("123456789");
            driver.Value.FindElement(By.XPath("//div[@class = 'form-group'][5]/label/span/input")).Click();
            driver.Value.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            string alertText = driver.Value.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(alertText);

            IWebElement link = driver.Value.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            string hrefAttr = link.GetAttribute("href");
            string expectedUrl = "https://rahulshettyacademy.com/documents-request";
            Assert.That(hrefAttr, Is.EqualTo(expectedUrl));
        }
    }
}
