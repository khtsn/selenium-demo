using SeleniumBasic.Common;

using OpenQA.Selenium;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class ChangePasswordPage : GeneralPage
    {
        #region Locators
        static readonly By _txtCurrentPassword = By.XPath("//input[@id='currentPassword']");
        static readonly By _txtNewPassword = By.XPath("//input[@id='newPassword']");
        static readonly By _txtConfirmPassword = By.XPath("//input[@id='confirmPassword']");

        static readonly By _lblSuccessMessage = By.XPath("//form[@id='ChangePW']//p[@class='message success']");
        static readonly By _lblErrorMessage = By.XPath("//form[@id='ChangePW']//p[@class='message error']");

        static readonly By _btnChangePassword = By.XPath("//input[@type='submit' and @value='Change Password']");
        #endregion

        #region Elements
        public IWebElement TxtCurrentPassword
        {
            get { return Constant.WebDriver.FindElement(_txtCurrentPassword); }
        }
        public IWebElement TxtNewPassword
        {
            get { return Constant.WebDriver.FindElement(_txtNewPassword); }
        }
        public IWebElement TxtConfirmPassword
        {
            get { return Constant.WebDriver.FindElement(_txtConfirmPassword); }
        }
        public IWebElement BtnChangePassword
        {
            get { return Constant.WebDriver.FindElement(_btnChangePassword); }
        }
        public IWebElement LblSuccessMessage
        {
            get { return Constant.WebDriver.FindElement(_lblSuccessMessage); }
        }
        public IWebElement LblErrorMessage
        {
            get { return Constant.WebDriver.FindElement(_lblErrorMessage); }
        }
        #endregion

        #region Methods
        public ChangePasswordPage ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            //input values
            TxtCurrentPassword.Clear();
            TxtCurrentPassword.SendKeys(oldPassword);
            TxtNewPassword.Clear();
            TxtNewPassword.SendKeys(newPassword);
            TxtConfirmPassword.Clear();
            TxtConfirmPassword.SendKeys(confirmPassword);

            //submit
            BtnChangePassword.Click();
            
            return this;
        }

        public string GetSuccessMessage()
        {
            return LblSuccessMessage.Text;
        }
        public string GetErrorMessage()
        {
            return LblErrorMessage.Text;
        }
        #endregion
    }
}
