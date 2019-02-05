using SeleniumBasic.Common;

using OpenQA.Selenium;

namespace SeleniumBasic.PageObjects
{
    class GeneralPage
    {
        #region Locators
        static readonly By _tabLogin = By.XPath("//div[@id='menu']//a[@href='/Account/Login.cshtml']");
        static readonly By _tabLogout = By.XPath("//div[@id='menu']//a[@href='/Account/Logout']");
        static readonly By _tabRegister = By.XPath("//div[@id='menu']//a[@href='/Account/Register.cshtml']");
        static readonly By _tabChangePassword = By.XPath("//div[@id='menu']//a[@href='/Account/ChangePassword.cshtml']");
        static readonly By _tabMyTicket = By.XPath("//div[@id='menu']//a[@href='/Page/ManageTicket.cshtml']");

        static readonly By _lblWelcomeMessage = By.XPath("//div[@id='header']//div[@class='account']");
        #endregion

        #region Elements
        public IWebElement TabLogin
        {
            get { return Constant.WebDriver.FindElement(_tabLogin); }
        }
        public IWebElement TabLogout
        {
            get { return Constant.WebDriver.FindElement(_tabLogout); }
        }
        public IWebElement TabRegister
        {
            get { return Constant.WebDriver.FindElement(_tabRegister); }
        }
        public IWebElement TabChangePassword
        {
            get { return Constant.WebDriver.FindElement(_tabChangePassword); }
        }
        public IWebElement TabMyTicket
        {
            get { return Constant.WebDriver.FindElement(_tabMyTicket); }
        }
        public IWebElement LblWelcomeMessage
        {
            get { return Constant.WebDriver.FindElement(_lblWelcomeMessage); }
        }
        #endregion

        #region Methods
        public string GetWelcomeMessage()
        {
            return LblWelcomeMessage.Text;
        }
        public LoginPage GoToLoginPage()
        {
            TabLogin.Click();
            return new LoginPage();
        }
        public RegisterPage GoToRegisterPage()
        {
            TabRegister.Click();
            return new RegisterPage();
        }
        public ChangePasswordPage GoToChangePasswordPage()
        {
            TabChangePassword.Click();
            return new ChangePasswordPage();
        }
        public MyTicketPage GoToMyTicketPage()
        {
            TabMyTicket.Click();
            return new MyTicketPage();
        }
        #endregion
    }
}
