using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using AngleSharp;
using System.Configuration;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.DevTools.V123.Page;


namespace C_SeleniumFramework.utilities
{
    internal class Base
    {
        public string browserName;
        public ExtentReports extentReports;
        public ExtentTest test;
        [OneTimeSetUp]
        public void SetupReports()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);

            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
            extentReports.AddSystemInfo("Host Name", "Localhost");
            extentReports.AddSystemInfo("Environment", "Test");
            extentReports.AddSystemInfo("Username", "Hayk");
        }

        public ThreadLocal<IWebDriver> driver = new();

        [SetUp]
        public void StartBrowser()
        {
            test = extentReports.CreateTest(TestContext.CurrentContext.Test.Name);

            browserName = TestContext.Parameters["browserName"];
            if(browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            InitBrowser(browserName);

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise";

        }

        public IWebDriver Driver { get { return driver.Value; } }
        private void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
                default:
                    Console.WriteLine("Unknown Browser");
                    break;
                    
            }
        }

        public static JsonReader GetDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            if (status == TestStatus.Passed)
            {
                test.Log(Status.Pass, "Test passed" + stackTrace);
            }
            else if(status == TestStatus.Failed)
            {
                test.Fail("Test Failed", CaptureScreenshot(driver.Value));
                test.Log(Status.Fail, "Test failed with logTrace " + stackTrace);
            }
            extentReports.Flush();
            driver.Value?.Dispose();
        }

        public MediaEntityModelProvider CaptureScreenshot(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build();
        }
    }
}
