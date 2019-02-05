using OpenQA.Selenium;
using SeleniumBasic.Common;
using System.Collections.ObjectModel;
using SeleniumBasic.Entities;
using System.Collections.Generic;
using SeleniumBasic.Utilities;
using System;
using System.Linq;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class BookTicketSuccessPage : GeneralPage
    {
        private readonly string[] _tableOrder = {
            "DepartStation",
            "ArriveStation",
            "SeatType",
            "DepartDate",
            "BookDate",
            "ExpiredDate",
            "Amount",
            "TotalPrice" };

        #region Locators
        static readonly By _tblHeaders = By.XPath("//table[@class='MyTable WideTable']//th");
        static readonly By _tblContents = By.XPath("//table[@class='MyTable WideTable']//td");

        static readonly By _lblSuccessMessage = By.XPath("//h1[@align='center']");
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
        public IWebElement LblSuccessMessage
        {
            get { return Constant.WebDriver.FindElement(_lblSuccessMessage); }
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
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    ticket[_tableOrder[j]] = data[i, j].Text;
                }
                tickets.Add(ticket);
            }
            return tickets;
        }
        public string[] GetTableOrders()
        {
            return _tableOrder;
        }

        public string GetSuccessMessage()
        {
            return LblSuccessMessage.Text;
        }
        public bool CheckTicketExists(Ticket ticket)
        {
            return GetTickets().Any(
                a => a.DepartDate == ticket.DepartDate
                && a.DepartStation == ticket.DepartStation
                && a.ArriveStation == ticket.ArriveStation
                && a.SeatType == ticket.SeatType
                && a.Amount == ticket.Amount);
        }
        #endregion
    }
}
