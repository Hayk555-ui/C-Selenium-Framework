using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SeleniumFramework.pageObjects
{
    internal class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//div[@class = 'form-group'][5]/label/span/input")]
        private IWebElement checkbox;

        [FindsBy(How = How.Id, Using = "signInBtn")]
        private IWebElement signInButton;
        public IWebElement GetUsername() { return username; }

        public ProductsPage ValidLogin(string userName, string userPassword)
        {
            username.Clear();
            username.SendKeys(userName);
            password.Clear();
            password.SendKeys(userPassword);
            checkbox.Click();   
            signInButton.Click();
            return new ProductsPage(driver);
        }

    }
}
