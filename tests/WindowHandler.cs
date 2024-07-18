using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using C_SeleniumFramework.utilities;

namespace Selenium
{
    internal class WindowHandler : Base
    {

        [Test]
        public void WindowHandlerTest()
        {
            string email = "mentor@rahulshettyacademy.com";

            driver.Value.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material")).Click();
            var openedWindow = driver.Value.WindowHandles[1];
            driver.Value.SwitchTo().Window(openedWindow);
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.ClassName("red")).Text);

            string textToVerify = driver.Value.FindElement(By.CssSelector(".red")).Text;

            string[] splittedTextToVerify = textToVerify.Split("at");
            string[] trimmedText = splittedTextToVerify[1].Trim().Split(" ");
            Assert.That(email.Equals(trimmedText[0]));

        }

    }
}
