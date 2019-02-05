using OpenQA.Selenium;
using SeleniumBasic.Common;
using SeleniumBasic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class CheckPricePage : GeneralPage
    {
        #region Locators
        static readonly string _xpathBookTicket = "//td[text()='{0}']/following::a[@class='BoxLink']";
        #endregion

        #region Elements
        #endregion

        #region Methods
        public BookTicketPage BookTicket(Ticket ticket)
        {
            IWebElement element = Constant.WebDriver.FindElement(By.XPath(string.Format(_xpathBookTicket, ticket.SeatType)));
            element.Click();
            return new BookTicketPage();
        }
        #endregion
    }
}
