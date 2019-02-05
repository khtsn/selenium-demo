using OpenQA.Selenium;
using SeleniumBasic.Common;
using System.Collections.ObjectModel;
using SeleniumBasic.Entities;
using System.Collections.Generic;
using SeleniumBasic.Utilities;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class MyTicketPage : GeneralPage
    {
        private readonly string[] _tableOrder = {
            "No",
            "DepartStation",
            "ArriveStation",
            "SeatType",
            "DepartDate",
            "BookDate",
            "ExpiredDate",
            "Status",
            "Amount",
            "TotalPrice",
            "Operation" };

        #region Locators
        static readonly By _tblHeaders = By.XPath("//table[@class='MyTable']//th");
        static readonly By _tblContents = By.XPath("//table[@class='MyTable']//td");
        static readonly By _cmbDepartStation = By.XPath("//select[@name='FilterDpStation']");
        static readonly By _cmbArriveStation = By.XPath("//select[@name='FilterArStation']");

        static readonly By _btnApplyFilter = By.XPath("//input[@type='submit' and @value='Apply filter']");
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
        public IWebElement CmbDepartStation
        {
            get { return Constant.WebDriver.FindElement(_cmbDepartStation); }
        }
        public IWebElement CmbArriveStation
        {
            get { return Constant.WebDriver.FindElement(_cmbArriveStation); }
        }
        public IWebElement BtnApplyFilter
        {
            get { return Constant.WebDriver.FindElement(_btnApplyFilter); }
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
                for (int j = 0; j < data.GetLength(1) - 1; j++)
                {
                    ticket[_tableOrder[j]] = data[i, j].Text;
                }
                ticket.Operation = data[i, data.GetLength(1) - 1];
                tickets.Add(ticket);
            }
            return tickets;
        }
        public string[] GetTableOrders()
        {
            return _tableOrder;
        }
        public MyTicketPage ApplyFilter(TicketFilter filter)
        {
            if (!filter.DepartStation.Trim().Equals(""))
            {
                new SelectElement(CmbDepartStation).SelectByText(filter.DepartStation);
            }
            BtnApplyFilter.Click();
            return this;
        }
        public bool CheckTicketsExistWithFilter(TicketFilter ticketFilter)
        {
            return GetTickets().Any(a => a.DepartStation == ticketFilter.DepartStation);
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
        public MyTicketPage CancelTicket(Ticket ticket)
        {
            GetTickets().Where(
                a => a.DepartDate == ticket.DepartDate
                && a.DepartStation == ticket.DepartStation
                && a.ArriveStation == ticket.ArriveStation
                && a.SeatType == ticket.SeatType
                && a.Amount == ticket.Amount).FirstOrDefault()
                .Operation.Click();

            //confirm message
            Constant.WebDriver.SwitchTo().Alert().Accept();
            return this;
        }

        public void CancelAllTickets()
        {
            foreach(Ticket ticket in GetTickets()){
                CancelTicket(ticket);
            }
        }
        #endregion
    }
}
