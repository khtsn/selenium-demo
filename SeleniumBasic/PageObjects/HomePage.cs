using System.Collections.ObjectModel;

using SeleniumBasic.Common;

using OpenQA.Selenium;

namespace SeleniumBasic.PageObjects
{
    class HomePage : GeneralPage
    {
        #region Locators
        #endregion

        #region Elements

        #endregion

        #region Methods
        public HomePage Open()
        {
            Constant.WebDriver.Navigate().GoToUrl(Constant.HomePageURL);
            return this;
        }
        #endregion
    }
}
