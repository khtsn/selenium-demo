using SeleniumBasic.Common;

using OpenQA.Selenium;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class LoginPage : GeneralPage
    {
        #region Locators
        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//input[@value='login']");
        static readonly By _lblErrorMessage = By.XPath("//div[@id='content']//p[@class='message error LoginForm']");
        #endregion

        #region Elements
        public IWebElement TxtUsername
        {
            get { return Constant.WebDriver.FindElement(_txtUsername); }
        }

        public IWebElement TxtPassword
        {
            get { return Constant.WebDriver.FindElement(_txtPassword); }
        }
        public IWebElement BtnLogin
        {
            get { return Constant.WebDriver.FindElement(_btnLogin); }
        }
        #endregion
        public IWebElement LblErrorMessage
        {
            get { return Constant.WebDriver.FindElement(_lblErrorMessage); }
        }

        #region Methods
        public HomePage Login(string username, string password)
        {
            //submit creds
            TxtUsername.Clear();
            TxtUsername.SendKeys(username);
            TxtPassword.Clear();
            TxtPassword.SendKeys(password);
            BtnLogin.Click();

            //homepage return
            return new HomePage();
        }

        public LoginPage LoginInvalid(string username, string password)
        {
            //submit creds
            TxtUsername.Clear();
            TxtUsername.SendKeys(username);
            TxtPassword.Clear();
            TxtPassword.SendKeys(password);
            BtnLogin.Click();

            //homepage return
            return this;
        }

        public string GetErrorMessage()
        {
            return LblErrorMessage.Text;
        }
        #endregion
    }
}
