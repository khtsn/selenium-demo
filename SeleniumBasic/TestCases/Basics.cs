using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SeleniumBasic.Common;
using SeleniumBasic.PageObjects;
using SeleniumBasic.Utilities;

using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace SeleniumBasic.TestCases
{
    [TestClass]
    public class Basics
    {
        [TestInitialize]
        public void SetUp()
        {
            Console.WriteLine("Run test initialize");

            //start firefox
            Constant.WebDriver = new FirefoxDriver();
            //maximize
            Constant.WebDriver.Manage().Window.Maximize();
        }

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
            Console.WriteLine("TC02 - User can't login with blank 'Username' textbox");

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
            string actualMsg = loginPage.LoginInvalid(Constant.ValidUsername, "invalidpassword").GetErrorMessage();
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
            string email = String.Format("khanh{0}@gmail.com", StringUtils.RandomString(8, true));
            string password = StringUtils.RandomString(8, true);
            string confirm = StringUtils.RandomString(8, true);
            string pid = StringUtils.RandomString(8, true);

            string actualMsg = registerPage.Register(email, password, confirm, pid).GetThankYouMessage().Trim();
            string expectedMsg = "Thank you for registering your account";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC06_CannotLoginWithoutActivation()
        {
            Console.WriteLine("TC06 - User can't login with account hasn't been activated");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Login" tab
            LoginPage loginPage = homePage.GoToLoginPage();

            //3.Enter valid Email and invalid Password 
            //4.Click on "Login" button
            //VP. User can't login and message "There was a problem with your login and/or errors exist in your form. " appears.
            string actualMsg = loginPage.LoginInvalid(Constant.ValidUsernameNoActive, Constant.ValidPasswordNoActive).GetErrorMessage();
            string expectedMsg = "Invalid username or password. Please try again.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC07_UserChangePassword()
        {
            Console.WriteLine("TC07 - User can change password");

            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with valid account
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(Constant.ValidUsername, Constant.ValidPassword);

            //3. Click on "Change Password" tab
            ChangePasswordPage changePasswordPage = homePage.GoToChangePasswordPage();

            //4. Enter valid value into all fields.
            //5. Click on "Change Password" button
            //VP. Message "Your password has been updated" appears.
            string newPassword = "newPassword";
            string actualMsg = changePasswordPage.ChangePassword(Constant.ValidPassword, newPassword, newPassword).GetSuccessMessage();
            string expectedMsg = "Your password has been updated!";
            Assert.AreEqual(expectedMsg, actualMsg);

            //Post-condition: Change password to the old one
            changePasswordPage.ChangePassword(newPassword, Constant.ValidPassword, Constant.ValidPassword);
        }

        [TestMethod]
        public void TC08_UserCannotChangePassword()
        {
            Console.WriteLine("TC08 - User can't change password when 'New Password' and 'Confirm Password' are different.");

            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with valid account
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(Constant.ValidUsername, Constant.ValidPassword);

            //3. Click on "Change Password" tab
            ChangePasswordPage changePasswordPage = homePage.GoToChangePasswordPage();

            //4. Enter valid information into "Current Password" textbox but enter "a123:"/{}!@$\" into "New Password" textbox and "b456:"/{}!@$\" into "Confirm Password" textbox.
            //5. Click on "Change Password" button
            //VP. Error message "Password change failed. Please correct the errors and try again." appears.
            string newPassword = "a123:\"/{}!@$\\";
            string confirmPassword = "b456:\"/{}!@$\\";
            string actualMsg = changePasswordPage.ChangePassword(Constant.ValidPassword, newPassword, confirmPassword).GetSuccessMessage();
            string expectedMsg = "Password change failed. Please correct the errors and try again.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC09_UserCannotCreateAccountWithDiffPass()
        {
            Console.WriteLine("TC09 - User can't create account with 'Confirm password' is not the same as 'Password'");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Register" tab
            RegisterPage registerPage = homePage.GoToRegisterPage();

            //3.Enter valid information into all fields except "Confirm password" is not the same as "Password"
            //4.Click on "Register" button
            //VP. New account is created and message "Thank you for registering your account" appears.
            string email = String.Format("khanh{0}@gmail.com", StringUtils.RandomString(8, true));
            string password = StringUtils.RandomString(8, true) + "password";
            string confirm = StringUtils.RandomString(8, true) + "confirm";
            string pid = StringUtils.RandomString(8, true);

            string actualMsg = registerPage.RegisterInvalid(email, password, confirm, pid).GetErrorMessage();
            string expectedMsg = "There're errors in the form. Please correct the errors and try again.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC10_UserBookOneTicket()
        {
            Console.WriteLine("TC10 - User can book 1 ticket at a time");
            HomePage homePage = new HomePage();
            homePage.Open();
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(Constant.ValidUsername, Constant.ValidPassword);
            MyTicketPage myTicketPage = homePage.GoToMyTicketPage();
            IWebElement[,] test = TableUtils.getTableData(myTicketPage.GetTableHeader(), myTicketPage.GetTableContent());

            //1. Navigate to QA Railway Website
            //2. Login with a valid account 
            //3. Click on "Book ticket" tab
            //4. Select a "Depart date" from the list
            //5. Select "Sài Gòn" for "Depart from" and "Nha Trang" for "Arrive at".
            //6. Select "Soft bed with air conditioner" for "Seat type"
            //7. Select "1" for "Ticket amount"
            //8. Click on "Book ticket" button
            //VP. Message "Ticket booked successfully!" displays.Ticket information display correctly (Depart Date, Depart Station, Arrive Station, Seat Type, Amount)
        }

        [TestCleanup]
        public void TearDown()
        {
            Console.WriteLine("Run test cleanup");

            //close browser
            Constant.WebDriver.Quit();
        }
    }
}
