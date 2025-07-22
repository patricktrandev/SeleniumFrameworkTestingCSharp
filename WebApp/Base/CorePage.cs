using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using MemoryZoneFrameworkTest.WebApp.utils;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MemoryZoneFrameworkTest.WebApp.Base
{
    public class CorePage
    {
        public static IWebDriver driver;
        public static WebDriverWait wait;
        public static Actions action;



        public static IWebDriver initDriver(BrowserType.Browser browser)
        {
            DriverOptions options;

            switch (browser)
            {
                case BrowserType.Browser.Chrome:
                    options = new ChromeOptions();
                    break;
                case BrowserType.Browser.Firefox:
                    options = new FirefoxOptions();
                    break;
                case BrowserType.Browser.IE:
                    options = new EdgeOptions(); // or InternetExplorerOptions if using IE
                    break;
                default:
                    throw new ArgumentException("Invalid Browser");
            }

            //switch (browser)
            //{
            //    case BrowserType.Browser.Chrome:
            //        driver = new ChromeDriver();
            //        break;
            //    case BrowserType.Browser.Firefox:
            //        driver = new FirefoxDriver();
            //        break;
            //    case BrowserType.Browser.IE:
            //        driver = new EdgeDriver();
            //        break;
            //    default:

            //        throw new ArgumentException("Invalid Browser");
            //}


            //driver.Manage().Window.Maximize();


            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
            driver.Manage().Window.Maximize();


            return driver;
        }


        public static void closeDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        public void pressEnter()
        {
            action = new Actions(driver);
            action.SendKeys(Keys.Enter).Perform();

        }

        public void simulateCtrlClick(IWebElement el)
        {
            action = new Actions(driver);
            action.KeyDown(Keys.Control).Click(el).KeyUp(Keys.Control).Build().Perform();
        }

        public void hover(By by)
        {
            action = new Actions(driver);
            IWebElement el = driver.FindElement(by);
            action.MoveToElement(el).Perform();
        }
        public void hoverElement (IWebElement el)
        {
            action = new Actions(driver);
            
            action.MoveToElement(el).Perform();
        }
        
        public List<IWebElement> findListElement(By by)
        {
            waitPresenceElementToVisible(by);
            return driver.FindElements(by).ToList();
        }
        public IWebElement findElement(By by)
        {
            waitPresenceElementToVisible(by);
            return driver.FindElement(by);
        }
        public void Write(By by, string data, string stepDetails = "N/A")
        {
            driver.FindElement(by).SendKeys(data);

        }
        public void Click(By by)
        {
            driver.FindElement(by).Click();
        }

        public void openUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        public string getText(By by)
        {
            return driver.FindElement(by).Text;
        }


        public IWebElement waitPresenceElementToVisible(By locator)
        {
            WebDriverWait webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait = webDriverWait;
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }


        public void waitInvisibilityOfElementLocated(By locator)
        {
            WebDriverWait webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait = webDriverWait;
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }


        public void clickJs(By locator)
        {
            IWebElement el = driver.FindElement(locator);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", el);
        }
        public void clickJsWebElement(IWebElement el)
        {
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", el);
        }
        public void waitUntilDocumentIsReady()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public void ScrollToView(By locator)
        {
            IWebElement el = driver.FindElement(locator);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", el);
        }
    }
}
