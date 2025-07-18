using MemoryZoneFrameworkTest.WebApp.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryZoneFrameworkTest.WebApp.Page
{
    public class RegisterPage: CorePage
    {

        By buttonAccount = By.XPath("(//a[@title='Tài khoản'][contains(text(),'Tài khoản')])[1]");
        By linkRegister = By.XPath("//a[@class='btn-link-style btn-register']");
        By inputLastName = By.Id("lastName");
        By inputFirstName = By.Id("firstName");
        By inputPhone = By.Id("Phone");
        By inputEmail = By.Id("email");
        By inputPassword = By.Id("password");
        By buttonRegister = By.XPath("//button[@value='Đăng ký']");
        By textEmailBy = By.CssSelector("div[class='form-signup name-account m992'] p:last-of-type");


        public void goToPage(string url)
        {
            openUrl(url);
        }
        public void clickAccountTab()
        {
            waitPresenceElementToVisible(buttonAccount);
            clickJs(buttonAccount);
        }

        public void goToRegisterTab(string lname, string fname, string phone, string email, string pass)
        {
            
            clickJs(linkRegister);
            waitPresenceElementToVisible(inputLastName);
            Write(inputLastName, lname);
            Write(inputFirstName, fname);
            Write(inputPhone, phone);
            Write(inputEmail, email);
            Write(inputPassword, pass);
            Thread.Sleep(5000);
            clickJs(buttonRegister);
            
            //Thread.Sleep(7000);
        }

        public string getEmailInAccountTab()
        {
            waitPresenceElementToVisible(buttonAccount);
            Click(buttonAccount);
            waitPresenceElementToVisible(textEmailBy);
            String textEmail = getText(textEmailBy);

            return textEmail;
        }

    }
}
