using MemoryZoneFrameworkTest.WebApp.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryZoneFrameworkTest.WebApp.Page
{
    public class ProductDetailsPage:CorePage
    {

        By productList = By.CssSelector(".ae-search-product-item");
        By productName = By.CssSelector("h3");
        By addToCartButton = By.XPath("//div[@class='action-new']//button[contains(@class,'add_to_cart')]");
        By viewCartButton = By.XPath("//div[@class='cart-action']/a[@href='/cart']");

        
        By cartIcon = By.XPath("//div[contains(@class,'mini-cart')]");
        By itemNameInCart = By.CssSelector(".title-product-cart-mobile a");
        By quantity = By.XPath("//div[contains(@class,'form_product_content type1')]//label[contains(text(),'Số lượng:')]");
        By checkoutButton = By.XPath("//span[contains(text(),'Tiến hành thanh toán')]");
        By price = By.CssSelector(".cart-price span");
        By total = By.XPath("//span[contains(@class,'totals_price_mobile')]");

        public void addProductToCart(List<String> products)
        {
            List<IWebElement> listOfProduct = findListElement(productList);

            foreach (var p in listOfProduct)
            {
                var name=p.FindElement(productName);
                //clickJsWebElement(name);
                if (products.Contains(name.Text))
                {
                    simulateCtrlClick(name);
                }

            }

            var originalTab = driver.CurrentWindowHandle;
            var allTabs = driver.WindowHandles;
            int count = 0;
            foreach (var tab in allTabs)
            {
                if(tab != originalTab)
                {
                    driver.SwitchTo().Window(tab);
                    
                    try
                    {
                        
                        waitPresenceElementToVisible(addToCartButton);
                        ScrollToView(quantity);
                        clickJs(addToCartButton);
                        waitPresenceElementToVisible(viewCartButton);
                        clickJs(viewCartButton);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Tab {tab} error: {ex.Message}");
                    }
                    count++;
                    driver.Close();
                }
                
                driver.SwitchTo().Window(originalTab);
               
            }
            driver.Navigate().Refresh();
            //waitUntilDocumentIsReady();
            hover(cartIcon);
            clickJs(checkoutButton);
        }


        public List<String> verifyItemsInCart()
        {
            
            waitPresenceElementToVisible(itemNameInCart);
            List<IWebElement> itemsInCart = findListElement(itemNameInCart);
           
            List<String> names = new List<String>();
            foreach (var item in itemsInCart)
            {
                names.Add(item.GetAttribute("title"));
            }
            return names;
        }
        public double verifyItemsPriceInCart()
        {

            waitPresenceElementToVisible(itemNameInCart);
            List<IWebElement> itemsPriceInCart = findListElement(price);
            double p = 0;
            foreach (var item in itemsPriceInCart)
            {
                
                p += Convert.ToDouble(item.Text.Split(' ')[0].Replace(".", ""));
            }
            return p;
        }

        public double getActualTotal()
        {
            return Convert.ToDouble(getText(total).Split(' ')[0].Replace(".", ""));
        }
    }
}
