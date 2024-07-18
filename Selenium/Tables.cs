using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium
{
    internal class Tables
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }

        [Test]
        public void SortVeggies()
        {
            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");

            ArrayList arrayList = [];

            IList<IWebElement> listOfVeggies = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (var item in listOfVeggies)
            {
                arrayList.Add(item.Text);
            }

            TestContext.Progress.WriteLine(arrayList);
            arrayList.Sort();

            TestContext.Progress.WriteLine("List after sorting" + arrayList);

            driver.FindElement(By.XPath("//th[contains(@aria-label, 'fruit name')]")).Click();

            ArrayList arrayList2 = [];

            IList<IWebElement> listOfSortedVeggies = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (var item in listOfSortedVeggies)
            {
                arrayList2.Add(item.Text);
            }
            Assert.That(arrayList, Is.EqualTo(arrayList2));          
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
