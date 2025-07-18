using MemoryZoneFrameworkTest.WebApp.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V136.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryZoneFrameworkTest.WebApp.Page
{
    public class SearchProductPage:CorePage
    {

        By inputSearch = By.CssSelector(".ae-search-group__input");
        By itemBox = By.CssSelector(".ae-search-product-item");
        By textResultBy = By.CssSelector(".title-head");
        //category
        By categoryBoxSearch = By.CssSelector("div[class*='toogle-nav-wrapper']:first-child");
        By screenTab = By.XPath("//div[@class='navigation-wrapper ']//ul/li/a[@href='/man-hinh']");
        By screenSamsungTab = By.XPath("//div[@class='toogle-nav-wrapper']//li[4]//div[@class='submenu scroll']//ul/li[1]/span[6]/a[@title='Samsung']");
        By titleResult = By.CssSelector("div[class*='card'] h1");


        public void searchProduct(string keyword)
        {
            Click(inputSearch);
            Write(inputSearch, keyword);
            pressEnter();
        }

        public string getResultText()
        {
            waitPresenceElementToVisible(itemBox);
            return getText(textResultBy);

        }

        public void searchProductByCategory()
        {
            waitPresenceElementToVisible(categoryBoxSearch);
            hover(categoryBoxSearch);
            waitPresenceElementToVisible(screenTab);
            hover(screenTab);
            hover(screenSamsungTab);
            Click(screenSamsungTab);

           
        }

        public string getResultSearchByCategory()
        {
            waitPresenceElementToVisible(titleResult);
            return getText(titleResult);
        }

       

    }
}
    