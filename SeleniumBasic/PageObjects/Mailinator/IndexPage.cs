using SeleniumBasic.Common;
using OpenQA.Selenium;
using SeleniumBasic.PageObjects.QARailway;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumBasic.PageObjects.Mailinator
{
    public class IndexPage
    {
        #region Locators
        static readonly By _lblConfirmEmail = By.XPath("//div[@class='innermail ng-binding' and contains(text(), 'Please confirm your account')]");
        static readonly By _lnkConfirm = By.XPath("//a[@href]");
        static readonly By _iframeMail = By.XPath("//iframe[@id='publicshowmaildivcontent']");
        #endregion

        #region Elements
        public IWebElement LblConfirmEmail
        {
            get { return Constant.WebDriver.FindElement(_lblConfirmEmail); }
        }
        public IWebElement LnkConfirm
        {
            get { return Constant.WebDriver.FindElement(_lnkConfirm); }
        }
        public IWebElement IframeMail
        {
            get { return Constant.WebDriver.FindElement(_iframeMail); }
        }
        #endregion

        #region Methods
        public IndexPage Open(string username)
        {
            Constant.WebDriver.Navigate().GoToUrl(string.Format(Constant.MailinatorPageURL, username));
            return this;
        }
        public IndexPage ClickConfirmEmail()
        {
            LblConfirmEmail.Click();
            return this;
        }
        public RegisterConfirmedPage ClickConfirmLink()
        {
            int timeoutInSeconds = 15;

            Constant.webDriverWait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            IWebDriver webDriver = Constant.WebDriver.SwitchTo().Frame(Constant.webDriverWait.Until(a => a.FindElement(_iframeMail)));
            WebDriverWait webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            Constant.WebDriver.Navigate().GoToUrl(webDriverWait.Until(a => a.FindElement(_lnkConfirm)).Text);

            return new RegisterConfirmedPage();
        }
        #endregion
    }
}
