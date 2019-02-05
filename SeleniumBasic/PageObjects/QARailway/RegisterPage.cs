using SeleniumBasic.Common;

using OpenQA.Selenium;
using SeleniumBasic.Entities;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class RegisterPage : GeneralPage
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
        public ThankYouRegisterPage Register(User user)
        {
            //input data
            TxtEmail.Clear();
            TxtEmail.SendKeys(user.Email);
            TxtPassword.Clear();
            TxtPassword.SendKeys(user.Password);
            TxtConfirmPassword.Clear();
            TxtConfirmPassword.SendKeys(user.PasswordConfirm);
            TxtPIDPassport.Clear();
            TxtPIDPassport.SendKeys(user.Pid);

            //submit
            BtnRegister.Click();

            //return thank you page
            return new ThankYouRegisterPage();
        }
        public RegisterPage RegisterInvalid(User user)
        {
            //input data
            TxtEmail.Clear();
            TxtEmail.SendKeys(user.Email);
            TxtPassword.Clear();
            TxtPassword.SendKeys(user.Password);
            TxtConfirmPassword.Clear();
            TxtConfirmPassword.SendKeys(user.PasswordConfirm);
            TxtPIDPassport.Clear();
            TxtPIDPassport.SendKeys(user.Pid);

            //submit
            BtnRegister.Click();

            //return error page
            return this;
        }
        public string GetErrorMessage()
        {
            return LblErrorMessage.Text;
        }
        #endregion
    }
}
