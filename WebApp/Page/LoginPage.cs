using MemoryZoneFrameworkTest.WebApp.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryZoneFrameworkTest.WebApp.Page
{
    public class LoginPage: CorePage
    {
        By inputEmail=By.Id("customer_email");
        By inputPassword= By.Id("customer_password");
        By buttonLogin= By.XPath("//button[@value='Đăng nhập']");

        public void loginAccount(string email, string pass)
        {
            waitPresenceElementToVisible(inputEmail);
            Write(inputEmail, email);
            Write(inputPassword, pass);
            Thread.Sleep(5100);
            clickJs(buttonLogin);
        }
    }
}
