using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SeleniumFramework.pageObjects
{
    internal class ProductsPage
    {
        private IWebDriver driver;
        By cardTitle = By.CssSelector(".card-title a");
        By buttonCard = By.CssSelector(".card-footer button");
        public ProductsPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkoutButton;

        public void WaitForPageToDisplay(int secondToWait)
        {
            WebDriverWait wait = new (driver, TimeSpan.FromSeconds(secondToWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.PartialLinkText("Checkout")));
        }

        public IList<IWebElement> GetCards()
        {
            return cards;
        }

        public By GetCardTitle()
        {
            return cardTitle;
        }

        public CheckoutPage CheckoutButton()
        {
            checkoutButton.Click();
            return new CheckoutPage(driver);
        }
        public By AddToCardButton()
        {
            return buttonCard;
        }
    }
}
