using AventStack.ExtentReports;
using MemoryZoneFrameworkTest.WebApp.Base;
using MemoryZoneFrameworkTest.WebApp.Helper;
using MemoryZoneFrameworkTest.WebApp.Model;
using MemoryZoneFrameworkTest.WebApp.Page;

namespace MemoryZoneFrameworkTest.Tests.AppTests
{
    [TestFixture]
    public class FeatureTest:BaseTest
    {
        
        RegisterPage registerPage= new RegisterPage();
        LoginPage loginPage = new LoginPage();
        SearchProductPage searchProductPage= new SearchProductPage();
        CompareItemPage compareItemPage= new CompareItemPage();
        ProductDetailsPage productDetailsPage= new ProductDetailsPage();

        

        

        [Test]
        [TestCaseSource(nameof(LoginWithJsonDataSource))]
        public void SearchProductTestFromInputSearch_TC003(AppEntity loginModel)
        {

            string email = loginModel.UserName;
            string pass = loginModel.Password;
            string keyword = "laptop msi";
            ExtentManager.Step = ExtentManager.Test.CreateNode("Navigate to website");
            registerPage.goToPage("https://memoryzone.com.vn/");
            ExtentManager.Step.Pass("Navigated to login page");
            registerPage.clickAccountTab();
            
            loginPage.loginAccount(email, pass);
            searchProductPage.searchProduct(keyword);
            ExtentManager.Step = ExtentManager.Test.CreateNode("Enter keyword to search");
            String txt=searchProductPage.getResultText();
            Assert.IsTrue(txt.Contains(keyword.ToUpper()));
            ExtentManager.Test.Log(Status.Pass, "Return a list of product successfully");
        }

        [Test]
        [TestCaseSource(nameof(LoginWithJsonDataSource))]
        public void SearchProductTestFromCategory_TC004(AppEntity loginModel)
        {

            string email = loginModel.UserName;
            string pass = loginModel.Password;
            string keyword = "laptop msi";
            ExtentManager.Step = ExtentManager.Test.CreateNode("Navigate to website");
            registerPage.goToPage("https://memoryzone.com.vn/");
            ExtentManager.Step.Pass("Navigated to login page");
            registerPage.clickAccountTab();
            ExtentManager.Step.Pass("Enter valid credentials");
            loginPage.loginAccount(email, pass);
            ExtentManager.Step = ExtentManager.Test.CreateNode("Hover menu to find product to search");
            searchProductPage.searchProductByCategory();

            String txt=searchProductPage.getResultSearchByCategory();
            Assert.IsTrue(txt.Contains("Samsung"));
            ExtentManager.Test.Log(Status.Pass, "Return a list of product including selected brand name");
        }

        [Test]
        [Category("Smoke")]
        public void CompareProductWithoutCredentialTest_TC005()
        {

            ExtentManager.Step = ExtentManager.Test.CreateNode("Navigate to website");
            registerPage.goToPage("https://memoryzone.com.vn/");
            ExtentManager.Step = ExtentManager.Test.CreateNode("Enter keyword to search");
            compareItemPage.searchProductByCategoryWithoutLogin();
            ExtentManager.Step.Pass("Return a list of product successfully.");
            compareItemPage.pickItemsToCompare(2);
            ExtentManager.Step = ExtentManager.Test.CreateNode("Select item to compare");
            List<String> products = compareItemPage.getProductsNameList();

            Assert.AreEqual(2, products.Count());
            ExtentManager.Test.Log(Status.Pass, "Comparison page shows both selected products and total count of selected product is 2");
        }


        [Test]
        [Category("Smoke")]
        public void AddProductToCart_TC006()
        {
            List<String> products = new List<string> 
            { "Laptop MSI Modern 14 F13MG-247VN (i7-1355U, Iris Xe Graphics, Ram 8GB DDR4, SSD 512GB, 14 Inch IPS FHD)",
            "Laptop MSI Prestige 16 AI+ Evo B2VMG-016VN (Ultra 9 288V, Arc Graphics, RAM 32GB LPDDR5X, SSD 1TB, 16 Inch OLED UHD+ 60Hz 100% DCI-P3)"};

            
            string keyword = "laptop msi";
            ExtentManager.Step = ExtentManager.Test.CreateNode("Navigate to website");
            registerPage.goToPage("https://memoryzone.com.vn/");
            ExtentManager.Step = ExtentManager.Test.CreateNode("Enter keyword to search");
            searchProductPage.searchProduct(keyword);
            ExtentManager.Step.Pass("Return a list of product successfully.");
            searchProductPage.getResultText();
            ExtentManager.Step = ExtentManager.Test.CreateNode("Add item to cart");
            productDetailsPage.addProductToCart(products);
            //verify product name
            List<String> items = productDetailsPage.verifyItemsInCart();
            Assert.AreEqual(products.Count, items.Count());
            //verify product price in total
            Double total = productDetailsPage.verifyItemsPriceInCart();
            Double actualTotal = productDetailsPage.getActualTotal();
            Assert.That(total, Is.EqualTo(actualTotal));
            ExtentManager.Test.Log(Status.Pass, "Cart contains correct product names and total price matches the sum of selected products");
        }

    }
}