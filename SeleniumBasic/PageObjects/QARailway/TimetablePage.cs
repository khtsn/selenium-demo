using OpenQA.Selenium;
using SeleniumBasic.Common;
using System.Collections.ObjectModel;
using SeleniumBasic.Entities;
using System.Collections.Generic;
using SeleniumBasic.Utilities;
using System.Linq;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class TimetablePage : GeneralPage
    {
        private readonly string[] _tableOrder = {
            "No",
            "DepartStation",
            "ArriveStation",
            "DepartTime",
            "ArriveTime",
            "CheckPrice",
            "BookTicket" };

        #region Locators
        static readonly By _tblHeaders = By.XPath("//table[@class='MyTable WideTable']//th");
        static readonly By _tblContents = By.XPath("//table[@class='MyTable WideTable']//td");
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
            List<Ticket> tickets = new List<Ticket>();
            for (int i = 1; i < data.GetLength(0); i++)
            {
                Ticket ticket = new Ticket();
                for (int j = 0; j < data.GetLength(1) - 2; j++)
                {
                    ticket[_tableOrder[j]] = data[i, j].Text;
                }
                ticket.CheckPrice = data[i, data.GetLength(1) - 2];
                ticket.BookTicket = data[i, data.GetLength(1) - 1];
                tickets.Add(ticket);
            }
            return tickets;
        }
        public string[] GetTableOrders()
        {
            return _tableOrder;
        }
        public BookTicketPage BookTicket(Ticket ticket)
        {
            List<Ticket> tickets = GetTickets();
            Ticket result = tickets.Where(
                a => a.DepartStation == ticket.DepartStation
                && a.ArriveStation == ticket.ArriveStation).First();
            result.BookTicket.Click();
            return new BookTicketPage();
        }
        #endregion
    }
}
