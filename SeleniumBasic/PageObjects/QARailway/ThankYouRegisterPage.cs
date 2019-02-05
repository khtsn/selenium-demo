using SeleniumBasic.Common;

using OpenQA.Selenium;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class ThankYouRegisterPage : GeneralPage
    {
        #region Locators
        static readonly By _lblThankYouMessage = By.XPath("//div[@id='content']//h1[@align='center']");
        #endregion

        #region Elements
        public IWebElement LblThankYouMessage
        {
            get { return Constant.WebDriver.FindElement(_lblThankYouMessage); }
        }
        #endregion

        #region Methods
        public string GetThankYouMessage()
        {
            return LblThankYouMessage.Text;
        }
        #endregion
    }
}
