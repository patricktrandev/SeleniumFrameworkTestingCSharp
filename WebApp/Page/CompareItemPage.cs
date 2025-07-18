using MemoryZoneFrameworkTest.WebApp.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V136.Debugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryZoneFrameworkTest.WebApp.Page
{
    public class CompareItemPage:CorePage
    {
        By menuItemScreen = By.XPath("//div[contains(@class,'col-lg-3')]//a[@title='Màn hình - Loa']");
        By menuSamsungScreenTab = By.XPath("//section[@class='section awe-section-1']//li[4]//div[@class='submenu scroll']//ul/li[1]/span[6]/a[@title='Samsung']");
        By titleResult = By.CssSelector("div[class*='card'] h1");
        By hideCompareTabButton = By.CssSelector(".js-compare-product-hide-qv");
        By productNameList = By.CssSelector(".compare-product__item h3");
        

        By productList = By.CssSelector("div.product-thumbnail");
        By productImg = By.CssSelector("img:first-of-type");
        By compareIcon = By.CssSelector(".group_action a");


        By buttonCompare = By.XPath("//a[@class='btn btn-main']");

        public void searchProductByCategoryWithoutLogin()
        {
            waitPresenceElementToVisible(menuItemScreen);
            hover(menuItemScreen);
            Click(menuSamsungScreenTab);
            
        }

        public void pickItemsToCompare(int num)
        {
            waitPresenceElementToVisible(titleResult);
            
            
            var products=findListElement(productList);
            //Console.WriteLine(products.Count);
            int count = 0;
            foreach (var p in products)
            {
                if (count == num) break;
                    
                var img = p.FindElement(productImg);
                hoverElement(img);
                Thread.Sleep(500);
                var compareButton = p.FindElement(compareIcon);
                clickJsWebElement(compareButton);
                waitPresenceElementToVisible(hideCompareTabButton);
                if (count <= num-1)
                {
                    clickJs(hideCompareTabButton);
                    
                }
                
                
                count++;
            }

            clickJs(buttonCompare);
            
        }

        public List<String> getProductsNameList()
        {
            waitPresenceElementToVisible(productNameList);
            List<IWebElement> names=findListElement(productNameList);
            List<String> itemNames= new List<String>();

            foreach (var item in names)
            {
                itemNames.Add(item.Text);
            }

            return itemNames;
        }

        
    }
}
