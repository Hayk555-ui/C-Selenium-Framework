using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SeleniumFramework.pageObjects
{
    internal class CheckoutPage
    {
        private readonly IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> checkoutCards;

        [FindsBy(How = How.XPath, Using = "//button[@class='btn btn-success']")]
        private IWebElement checkoutButton;

        public IList<IWebElement> GetCheckoutCards()
        {
            return checkoutCards;
        }
        public void Checkout()
        {
            checkoutButton.Click();
        }
    }
}
