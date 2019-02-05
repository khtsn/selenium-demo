using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SeleniumBasic.Common;
using SeleniumBasic.PageObjects.QARailway;
using SeleniumBasic.Entities;

namespace SeleniumBasic.TestCases
{
    [TestClass]
    public class BasicCases : BaseCase
    {
        [TestMethod]
        public void TC01_LoginValid()
        {
            Console.WriteLine("TC01 - User can log into Railway with valid username and password");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Login" tab
            LoginPage loginPage = homePage.GoToLoginPage();

            //3.Enter valid Email and Password
            //4.Click on "Login" button
            //VP. User is logged into Railway. Welcome user message is displayed
            string actualMsg = loginPage.Login(Constant.ValidUsername, Constant.ValidPassword).GetWelcomeMessage().Trim();
            string expectedMsg = "Welcome " + Constant.ValidUsername;
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC02_LoginInvalidBlankUsername()
        {
            Console.WriteLine("TC02 - User can't login with blank \"Username\" textbox");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Login" tab
            LoginPage loginPage = homePage.GoToLoginPage();

            //3. User doesn't type any words into "Username" textbox but enter valid information into "Password" textbox 
            //4.Click on "Login" button
            //VP. User can't login and message "There was a problem with your login and/or errors exist in your form. " appears.
            string actualMsg = loginPage.LoginInvalid("", Constant.ValidPassword).GetErrorMessage();
            string expectedMsg = "There was a problem with your login and/or errors exist in your form.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC03_LoginInvalidPassword()
        {
            Console.WriteLine("TC03 - User cannot log into Railway with invalid password");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Login" tab
            LoginPage loginPage = homePage.GoToLoginPage();

            //3.Enter valid Email and invalid Password 
            //4.Click on "Login" button
            //VP. User can't login and message "There was a problem with your login and/or errors exist in your form. " appears.
            string actualMsg = loginPage.LoginInvalid(Constant.ValidUsername, "").GetErrorMessage();
            string expectedMsg = "There was a problem with your login and/or errors exist in your form.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC04_LoginInvalidSeveralTimes()
        {
            Console.WriteLine("TC04 - System shows message when user enter wrong password several times");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Login" tab
            LoginPage loginPage = homePage.GoToLoginPage();

            //3.Enter valid information into "Username" textbox except "Password" textbox.
            //4.Click on "Login" button
            string actualMsg = loginPage.LoginInvalid(Constant.ValidUsername, "").GetErrorMessage();
            //5.Repeat step 3 three more times.
            for (int i = 0; i < 3; i++)
            {
                actualMsg = loginPage.LoginInvalid(Constant.ValidUsername, "").GetErrorMessage();
            }
            //VP. User can't login and message "You have used 4 out of 5 login attempts. After all 5 have been used, you will be unable to login for 15 minutes." appears.
            string expectedMsg = "You have used 4 out of 5 login attempts. After all 5 have been used, you will be unable to login for 15 minutes.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC05_CreateNewAccount()
        {
            Console.WriteLine("TC05 - User can create new account");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Register" tab
            RegisterPage registerPage = homePage.GoToRegisterPage();

            //3.Enter valid information into all fields
            //4.Click on "Register" button
            //VP. New account is created and message "Thank you for registering your account" appears.
            User user = new User();
            user.InitData();
            string actualMsg = registerPage.Register(user).GetThankYouMessage().Trim();
            string expectedMsg = "Thank you for registering your account";
            Assert.AreEqual(expectedMsg, actualMsg);
        }
    }
}
