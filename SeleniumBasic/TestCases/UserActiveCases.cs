using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SeleniumBasic.Common;
using SeleniumBasic.PageObjects.QARailway;
using SeleniumBasic.Entities;

using System.Collections.Generic;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumBasic.PageObjects.Mailinator;
using System.Threading;

namespace SeleniumBasic.TestCases
{
    [TestClass]
    public class UserActiveCases : BaseCase
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

            //go to mailinator
            //go to user inbox
            IndexPage indexPage = new IndexPage();
            RegisterConfirmedPage mailPage = indexPage.Open(user.Username).ClickConfirmEmail().ClickConfirmLink();
            actualMsg = mailPage.GetConfirmedMessage();
            expectedMsg = "Registration Confirmed! You can now log in to the site.";
            Assert.AreEqual(expectedMsg, actualMsg);
            //End Pre-condition
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
            homePage = loginPage.Login(user.Email, user.Password);

            //3. Click on "Change Password" tab
            ChangePasswordPage changePasswordPage = homePage.GoToChangePasswordPage();

            //4. Enter valid value into all fields.
            //5. Click on "Change Password" button
            //VP. Message "Your password has been updated" appears.
            string newPassword = "newPassword";
            string actualMsg = changePasswordPage.ChangePassword(user.Password, newPassword, newPassword).GetSuccessMessage();
            string expectedMsg = "Your password has been updated!";
            Assert.AreEqual(expectedMsg, actualMsg);

            //Post-condition: Change password to the old one
            changePasswordPage.ChangePassword(newPassword, user.Password, user.Password);
        }

        [TestMethod]
        public void TC08_UserCannotChangePassword()
        {
            Console.WriteLine("TC08 - User can't change password when \"New Password\" and \"Confirm Password\" are different.");

            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with valid account
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(user.Email, user.Password);

            //3. Click on "Change Password" tab
            ChangePasswordPage changePasswordPage = homePage.GoToChangePasswordPage();

            //4. Enter valid information into "Current Password" textbox but enter "a123:"/{}!@$\" into "New Password" textbox and "b456:"/{}!@$\" into "Confirm Password" textbox.
            //5. Click on "Change Password" button
            //VP. Error message "Password change failed. Please correct the errors and try again." appears.
            string newPassword = "a123:\"/{}!@$\\";
            string confirmPassword = "b456:\"/{}!@$\\";
            string actualMsg = changePasswordPage.ChangePassword(Constant.ValidPassword, newPassword, confirmPassword).GetErrorMessage();
            string expectedMsg = "Password change failed. Please correct the errors and try again.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC09_UserCannotCreateAccountWithDiffPass()
        {
            Console.WriteLine("TC09 - User can't create account with \"Confirm password\" is not the same as \"Password\"");

            //1.Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2.Click on "Register" tab
            RegisterPage registerPage = homePage.GoToRegisterPage();

            //3.Enter valid information into all fields except "Confirm password" is not the same as "Password"
            //4.Click on "Register" button
            //VP. New account is created and message "Thank you for registering your account" appears.
            User user = new User();
            user.InitData();
            user.Password += "pass";
            user.PasswordConfirm += "confirm";

            string actualMsg = registerPage.RegisterInvalid(user).GetErrorMessage();
            string expectedMsg = "There're errors in the form. Please correct the errors and try again.";
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void TC10_UserBookOneTicket()
        {
            Console.WriteLine("TC10 - User can book 1 ticket at a time");

            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with a valid account 
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(user.Email, user.Password);

            //3. Click on "Book ticket" tab
            BookTicketPage bookTicketPage = homePage.GoToBookTicketPage();

            //4. Select a "Depart date" from the list
            //5. Select "Sài Gòn" for "Depart from" and "Nha Trang" for "Arrive at".
            //6. Select "Soft bed with air conditioner" for "Seat type"
            //7. Select "1" for "Ticket amount"
            //8. Click on "Book ticket" button
            //VP. Message "Ticket booked successfully!" displays.Ticket information display correctly (Depart Date, Depart Station, Arrive Station, Seat Type, Amount)
            DateTime dateTime = DateTime.Now.AddDays(5);

            Ticket ticket = new Ticket();

            ticket.DepartDate = dateTime.ToString("M/d/yyyy");
            ticket.DepartStation = "Sài Gòn";
            ticket.ArriveStation = "Nha Trang";
            ticket.SeatType = "Soft bed with air conditioner";
            ticket.Amount = "1";

            BookTicketSuccessPage bookTicketSuccessPage = bookTicketPage.BookTicket(ticket);

            string actualMsg = bookTicketSuccessPage.GetSuccessMessage();
            string expectedMsg = "Ticket booked successfully!";
            Assert.AreEqual(expectedMsg, actualMsg);

            Assert.IsTrue(bookTicketSuccessPage.CheckTicketExists(ticket));

            //Clean-up: Cancel all tickets
            bookTicketSuccessPage.GoToMyTicketPage().CancelAllTickets();
        }

        [TestMethod]
        public void TC11_UserBookManyTickets()
        {
            Console.WriteLine("TC11 - User can book many tickets at a time");

            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with a valid account 
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(user.Email, user.Password);

            //3. Click on "Book ticket" tab
            BookTicketPage bookTicketPage = homePage.GoToBookTicketPage();

            //4. Select a "Depart date" from the list
            //5. Select "Nha Trang" for "Depart from" and "Sài Gòn" for "Arrive at".
            //6. Select "Soft seat with air conditioner" for "Seat type"
            //7. Select "5" for "Ticket amount"
            //8. Click on "Book ticket" button
            //VP. Message "Ticket booked successfully!" displays.Ticket information display correctly (Depart Date, Depart Station, Arrive Station, Seat Type, Amount)
            DateTime dateTime = DateTime.Now.AddDays(5);

            Ticket ticket = new Ticket();

            ticket.DepartDate = dateTime.ToString("M/d/yyyy");
            ticket.DepartStation = "Nha Trang";
            ticket.ArriveStation = "Sài Gòn";
            ticket.SeatType = "Soft seat with air conditioner";
            ticket.Amount = "5";

            BookTicketSuccessPage bookTicketSuccessPage = bookTicketPage.BookTicket(ticket);

            string actualMsg = bookTicketSuccessPage.GetSuccessMessage();
            string expectedMsg = "Ticket booked successfully!";
            Assert.AreEqual(expectedMsg, actualMsg);

            Assert.IsTrue(bookTicketSuccessPage.CheckTicketExists(ticket));

            //Clean-up: Cancel all tickets
            bookTicketSuccessPage.GoToMyTicketPage().CancelAllTickets();
        }

        [TestMethod]
        public void TC12_UserBooksTicketOnTimetable()
        {
            Console.WriteLine("TC12 - User can open \"Book ticket\" page by clicking on \"Book ticket\" link in \"Train timetable\"");

            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with a valid account 
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(user.Email, user.Password);

            //3. Click on "Timetable" tab
            TimetablePage timetablePage = homePage.GoToTimetablePage();

            //4. Click on "Book ticket" link of the route from "Huế" to "Sài Gòn"
            //VP. "Book ticket" page is loaded with correct for  "Depart from" and "Arrive at" values.
            Ticket ticket = new Ticket();
            ticket.DepartStation = "Huế";
            ticket.ArriveStation = "Sài Gòn";

            BookTicketPage bookTicketPage = timetablePage.BookTicket(ticket);
            string actualDepartFrom = bookTicketPage.GetDepartFromSelectedValue();
            Assert.AreEqual(actualDepartFrom, ticket.DepartStation);
            string actualArriveAt = bookTicketPage.GetArriveAtSelectedValue();
            Assert.AreEqual(actualArriveAt, ticket.ArriveStation);
        }
        [TestMethod]
        public void TC13_UserBooksTicketOnTicketPrice()
        {
            Console.WriteLine("TC13 - User can open \"Book ticket\" page by click on \"Book ticket\" link in \"Ticket price\"");
            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with a valid account 
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(user.Email, user.Password);

            //3. Click on "Ticket price" tab
            TicketPricePage ticketPricePage = homePage.GoToTicketPricePage();

            //4. Click on "Check price" link of ticket from "Nha Trang" to "Phan Thiết"
            Ticket ticket = new Ticket();
            ticket.DepartStation = "Nha Trang";
            ticket.ArriveStation = "Phan Thiết";
            CheckPricePage checkPricePage = ticketPricePage.CheckPrice(ticket);

            //5. Click on "Book ticket" button of "Soft bed with air conditioner"
            //VP. "Book ticket" page is loaded with correct for "Depart from", "Arrive at", and "Seat type".
            ticket.SeatType = "Soft bed with air conditioner";
            BookTicketPage bookTicketPage = checkPricePage.BookTicket(ticket);
            Assert.AreEqual(bookTicketPage.GetDepartFromSelectedValue(), ticket.DepartStation);
            Assert.AreEqual(bookTicketPage.GetArriveAtSelectedValue(), ticket.ArriveStation);
            Assert.AreEqual(bookTicketPage.GetSeatTypeSelectedValue(), ticket.SeatType);
        }
        [TestMethod]
        public void TC14_UserCancelsTicket()
        {
            Console.WriteLine("TC14 - User can cancel a ticket");
            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with a valid account 
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(user.Email, user.Password);

            //3. Book a ticket
            BookTicketPage bookTicketPage = homePage.GoToBookTicketPage();

            DateTime dateTime = DateTime.Now.AddDays(5);

            Ticket ticket = new Ticket();

            ticket.DepartDate = dateTime.ToString("M/d/yyyy");
            ticket.DepartStation = "Sài Gòn";
            ticket.ArriveStation = "Nha Trang";
            ticket.SeatType = "Soft bed with air conditioner";
            ticket.Amount = "1";

            BookTicketSuccessPage bookTicketSuccessPage = bookTicketPage.BookTicket(ticket);

            //4. Click on "My ticket" tab
            //5. Click on "Cancel" button of ticket which user want to cancel.
            //6. Click on "OK" button on Confirmation message "Are you sure?"
            //VP. The canceled ticket is disappeared.
            MyTicketPage myTicketPage = bookTicketSuccessPage.GoToMyTicketPage().CancelTicket(ticket);
            Assert.IsFalse(myTicketPage.CheckTicketExists(ticket));
        }
        [TestMethod]
        public void TC15_UserFilterWithDepartStation()
        {
            Console.WriteLine("TC15 - User can filter \"Manager ticket\" table with Depart Station");
            //1. Navigate to QA Railway Website
            HomePage homePage = new HomePage();
            homePage.Open();

            //2. Login with a valid account 
            LoginPage loginPage = homePage.GoToLoginPage();
            homePage = loginPage.Login(user.Email, user.Password);

            //3. Book more than 6 tickets with different Depart Stations
            string[] departStations = { "Sài Gòn", "Phan Thiết", "Nha Trang", "Đà Nẵng", "Quảng Ngãi", "Huế" };
            for (int i = 0; i < 6; i++)
            {
                //Book a ticket
                BookTicketPage bookTicketPage = homePage.GoToBookTicketPage();

                DateTime dateTime = DateTime.Now.AddDays(5);

                Ticket ticket = new Ticket();

                ticket.DepartDate = dateTime.ToString("M/d/yyyy");
                ticket.DepartStation = departStations[i];
                ticket.SeatType = "Soft bed with air conditioner";
                ticket.Amount = "1";

                BookTicketSuccessPage bookTicketSuccessPage = bookTicketPage.BookTicket(ticket);
            }

            //4. Click on "My ticket" tab
            //5. Select one of booked Depart Station in "Depart Station" dropdown list
            //6. Click "Apply filter" button
            //VP. "Manage ticket" table shows correct ticket(s)
            TicketFilter ticketFilter = new TicketFilter();
            ticketFilter.DepartStation = "Nha Trang";
            MyTicketPage myTicketPage = homePage.GoToMyTicketPage().ApplyFilter(ticketFilter);
            Assert.IsTrue(myTicketPage.CheckTicketsExistWithFilter(ticketFilter));

            //Clean-up: Cancel all tickets
            myTicketPage.GoToMyTicketPage().CancelAllTickets();
        }
    }
}
