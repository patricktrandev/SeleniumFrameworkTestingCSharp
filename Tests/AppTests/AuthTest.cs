using AventStack.ExtentReports;
using MemoryZoneFrameworkTest.WebApp.Helper;
using MemoryZoneFrameworkTest.WebApp.Model;
using MemoryZoneFrameworkTest.WebApp.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryZoneFrameworkTest.Tests.AppTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class AuthTest:BaseTest
    {
        RegisterPage registerPage = new RegisterPage();
        LoginPage loginPage = new LoginPage();



        [Test]
        public void RegisterTest_TC001()
        {
            string FName = "name";
            string LName = "nguyen";
            string email = "johnnam123@gmail.com";
            string phone = "0985456679";
            string pass = "johnnam123@";
            ExtentManager.Step = ExtentManager.Test.CreateNode("Navigate to website");
            registerPage.goToPage("https://memoryzone.com.vn/");
            ExtentManager.Step.Pass("Navigated to register page");
            registerPage.clickAccountTab();
            ExtentManager.Step.Pass("Enter valid credentials");
            registerPage.goToRegisterTab(LName, FName, phone, email, pass);
            string txt = registerPage.getEmailInAccountTab();
            Assert.IsTrue(txt.Contains(email));
            ExtentManager.Test.Log(Status.Pass, "Register successfully and navigate to home page");
        }
        [Test]
        //[TestCase("ericnguyen2021@gmail.com", "ericnguyen2021@")]
        [TestCaseSource(nameof(LoginWithJsonDataSource))]
        public void LoginWithValidUserTest_TC002(AppEntity loginModel)
        {

            string email = loginModel.UserName;
            string pass = loginModel.Password;



            ExtentManager.Step = ExtentManager.Test.CreateNode("Navigate to website");
            registerPage.goToPage("https://memoryzone.com.vn/");
            registerPage.clickAccountTab();
            ExtentManager.Step.Pass("Navigated to login page");
            ExtentManager.Step = ExtentManager.Test.CreateNode("Enter credentials");
            loginPage.loginAccount(email, pass);
            ExtentManager.Step.Pass("Enter valid credentials");
            String txt = registerPage.getEmailInAccountTab();
            Assert.IsTrue(txt.Contains(email));
            ExtentManager.Test.Log(Status.Pass, "Login successfully and navigate to profile page");
        }
    }
}
