using OpenQA.Selenium;
using SeleniumBasic.Common;
using SeleniumBasic.Entities;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class TicketPricePage : GeneralPage
    {
        #region Locators
        static readonly string _xpathCheckPrice = "//li[text()='{0} to {1}']/following::a[@class='BoxLink'][1]";
        #endregion

        #region Elements
        #endregion

        #region Methods
        public CheckPricePage CheckPrice(Ticket ticket)
        {
            IWebElement element = Constant.WebDriver.FindElement(By.XPath(string.Format(_xpathCheckPrice, ticket.DepartStation, ticket.ArriveStation)));
            element.Click();
            return new CheckPricePage();
        }
        #endregion
    }
}
