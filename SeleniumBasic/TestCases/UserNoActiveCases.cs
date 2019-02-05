using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using SeleniumBasic.Common;
using SeleniumBasic.Entities;
using SeleniumBasic.PageObjects.Mailinator;
using SeleniumBasic.PageObjects.QARailway;
using System;

namespace SeleniumBasic.TestCases
{
    [TestClass]
    public class UserNoActiveCases : BaseCase
    {
        private User user;

        [TestInitialize]
        public void SetUp()
        {
            Console.WriteLine("Run test initialize");

            //start firefox
            Constant.WebDriver = new FirefoxDriver();
            //maximize
            Constant.WebDriver.Manage().Window.Maximize();

            //Pre-condition: Create a new account but do not activate it
            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Register" tab
            RegisterPage registerPage = homePage.GoToRegisterPage();

            //3.Enter valid information into all fields
            //4.Click on "Register" button
            //VP. New account is created and message "Thank you for registering your account" appears.
            user = new User();
            user.InitData();
            string actualMsg = registerPage.Register(user).GetThankYouMessage().Trim();
            string expectedMsg = "Thank you for registering your account";
            Assert.AreEqual(expectedMsg, actualMsg);
            //End Pre-condition
        }

        [TestMethod]
        public void TC06_CannotLoginWithoutActivation()
        {
            Console.WriteLine("TC06 - User cannot login with account hasn't been activated");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Login" tab
            LoginPage loginPage = homePage.GoToLoginPage();

            //3.Enter valid Email and invalid Password 
            //4.Click on "Login" button
            //VP. User can't login and message "There was a problem with your login and/or errors exist in your form. " appears.
            string actualMsg = loginPage.LoginInvalid(user.Email, user.Password).GetErrorMessage();
            string expectedMsg = "Invalid username or password. Please try again.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }
    }
}
