using SeleniumBasic.Common;

using OpenQA.Selenium;

namespace SeleniumBasic.PageObjects
{
    class RegisterPage : GeneralPage
    {
        #region Locators
        static readonly By _txtEmail = By.XPath("//input[@id='email']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _txtConfirmPassword = By.XPath("//input[@id='confirmPassword']");
        static readonly By _txtPIDPassport = By.XPath("//input[@id='pid']");

        static readonly By _btnRegister = By.XPath("//input[@type='submit' and @value='Register']");

        static readonly By _lblErrorMessage = By.XPath("//div[@id='content']//p[@class='message error']");

        #endregion

        #region Elements
        public IWebElement TxtEmail
        {
            get { return Constant.WebDriver.FindElement(_txtEmail); }
        }
        public IWebElement TxtPassword
        {
            get { return Constant.WebDriver.FindElement(_txtPassword); }
        }
        public IWebElement TxtConfirmPassword
        {
            get { return Constant.WebDriver.FindElement(_txtConfirmPassword); }
        }
        public IWebElement TxtPIDPassport
        {
            get { return Constant.WebDriver.FindElement(_txtPIDPassport); }
        }
        public IWebElement BtnRegister
        {
            get { return Constant.WebDriver.FindElement(_btnRegister); }
        }
        public IWebElement LblErrorMessage
        {
            get { return Constant.WebDriver.FindElement(_lblErrorMessage); }
        }
        #endregion

        #region Methods
        public ThankYouRegisterPage Register(string email, string password, string confirm, string pid)
        {
            //input data
            TxtEmail.Clear();
            TxtEmail.SendKeys(email);
            TxtPassword.Clear();
            TxtPassword.SendKeys(password);
            TxtConfirmPassword.Clear();
            TxtConfirmPassword.SendKeys(confirm);
            TxtPIDPassport.Clear();
            TxtPIDPassport.SendKeys(pid);
            //submit
            BtnRegister.Click();
            //return thank you page
            return new ThankYouRegisterPage();
        }
        public RegisterPage RegisterInvalid(string email, string password, string confirm, string pid)
        {
            //input data
            TxtEmail.Clear();
            TxtEmail.SendKeys(email);
            TxtPassword.Clear();
            TxtPassword.SendKeys(password);
            TxtConfirmPassword.Clear();
            TxtConfirmPassword.SendKeys(confirm);
            TxtPIDPassport.Clear();
            TxtPIDPassport.SendKeys(pid);
            //submit
            BtnRegister.Click();
            //return thank you page
            return this;
        }
        public string GetErrorMessage()
        {
            return LblErrorMessage.Text;
        }
        #endregion
    }
}
