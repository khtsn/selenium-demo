using OpenQA.Selenium;
using SeleniumBasic.Common;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class RegisterConfirmedPage : GeneralPage
    {
        #region Locators
        static readonly By _lblConfirmedMessage = By.XPath("//div[@id='page']//div[@id='content']//p[text()='Registration Confirmed! You can now log in to the site.']");
        #endregion

        #region Elements
        public IWebElement LblConfirmedMessage
        {
            get { return Constant.WebDriver.FindElement(_lblConfirmedMessage); }
        }
        #endregion

        #region Methods
        public string GetConfirmedMessage()
        {
            return LblConfirmedMessage.Text;
        }
        #endregion
    }
}
