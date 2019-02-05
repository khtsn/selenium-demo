using OpenQA.Selenium;
using SeleniumBasic.Common;
using System.Collections.ObjectModel;
using SeleniumBasic.Entities;
using System.Collections.Generic;
using SeleniumBasic.Utilities;

namespace SeleniumBasic.PageObjects
{
    class MyTicketPage : GeneralPage
    {
        #region Locators
        static readonly By _tblHeaders = By.XPath("//table[@class='MyTable']//th");
        static readonly By _tblContents = By.XPath("//table[@class='MyTable']//td");
        #endregion

        #region Elements
        public ReadOnlyCollection<IWebElement> TblHeaders
        {
            get { return Constant.WebDriver.FindElements(_tblHeaders); }
        }
        public ReadOnlyCollection<IWebElement> TblContents
        {
            get { return Constant.WebDriver.FindElements(_tblContents); }
        }
        #endregion

        #region Methods
        public ReadOnlyCollection<IWebElement> GetTableHeader()
        {
            return TblHeaders;
        }
        public ReadOnlyCollection<IWebElement> GetTableContent()
        {
            return TblContents;
        }
        public List<Ticket> GetTickets()
        {
            IWebElement[,] data = TableUtils.getTableData(TblHeaders, TblContents);
            return null;
        }
        #endregion
    }
}
