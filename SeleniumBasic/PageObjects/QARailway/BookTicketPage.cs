using SeleniumBasic.Common;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumBasic.Entities;
using System;

namespace SeleniumBasic.PageObjects.QARailway
{
    public class BookTicketPage
    {
        private int timeoutInSeconds = 15;

        #region Locators
        static readonly By _cmbDepartDate = By.XPath("//select[@name='Date']");
        static readonly By _cmbDepartFrom = By.XPath("//select[@name='DepartStation']");
        static readonly By _cmbArriveAt = By.XPath("//select[@name='ArriveStation']");
        static readonly By _cmbSeatType = By.XPath("//select[@name='SeatType']");
        static readonly By _cmbTicketAmount = By.XPath("//select[@name='TicketAmount']");

        static readonly By _cmbDepartFromSelected = By.XPath("//select[@name='DepartStation']//option[@selected]");
        static readonly By _cmbArriveAtSelected = By.XPath("//select[@name='ArriveStation']//option[@selected]");
        static readonly By _cmbSeatTypeSelected = By.XPath("//select[@name='SeatType']//option[@selected]");

        static readonly string _xpathCmb = "//select[@name='{0}']//option[text()='{1}']";

        static readonly By _btnBookTicket = By.XPath("//input[@type='submit' and @value='Book ticket']");
        #endregion

        #region Elements
        public IWebElement CmbDepartDate
        {
            get { return Constant.WebDriver.FindElement(_cmbDepartDate); }
        }
        public IWebElement CmbDepartFrom
        {
            get { return Constant.WebDriver.FindElement(_cmbDepartFrom); }
        }
        public IWebElement CmbDepartFromSelected
        {
            get { return Constant.WebDriver.FindElement(_cmbDepartFromSelected); }
        }
        public IWebElement CmbArriveAt
        {
            get { return Constant.WebDriver.FindElement(_cmbArriveAt); }
        }
        public IWebElement CmbArriveAtSelected
        {
            get { return Constant.WebDriver.FindElement(_cmbArriveAtSelected); }
        }
        public IWebElement CmbSeatType
        {
            get { return Constant.WebDriver.FindElement(_cmbSeatType); }
        }
        public IWebElement CmbSeatTypeSelected
        {
            get { return Constant.WebDriver.FindElement(_cmbSeatTypeSelected); }
        }
        public IWebElement CmbTicketAmount
        {
            get { return Constant.WebDriver.FindElement(_cmbTicketAmount); }
        }
        public IWebElement BtnBookTicket
        {
            get { return Constant.WebDriver.FindElement(_btnBookTicket); }
        }
        #endregion

        #region Methods
        public BookTicketSuccessPage BookTicket(Ticket ticket)
        {
            //check values exist and select them
            Constant.webDriverWait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            if (ticket.DepartDate != null)
            {
                Constant.webDriverWait.Until(
                    u => u.FindElement(By.XPath(string.Format(_xpathCmb, "Date", ticket.DepartDate)
                    )));
                new SelectElement(CmbDepartDate).SelectByText(ticket.DepartDate);
            }
            if (ticket.DepartStation != null)
            {
                Constant.webDriverWait.Until(
                    u => u.FindElement(By.XPath(string.Format(_xpathCmb, "DepartStation", ticket.DepartStation)
                    )));
                new SelectElement(CmbDepartFrom).SelectByText(ticket.DepartStation);
            }
            if (ticket.ArriveStation != null)
            {
                Constant.webDriverWait.Until(
                    u => u.FindElement(By.XPath(string.Format(_xpathCmb, "ArriveStation", ticket.ArriveStation)
                    )));
                new SelectElement(CmbArriveAt).SelectByText(ticket.ArriveStation);
            }
            if (ticket.SeatType != null)
            {
                Constant.webDriverWait.Until(
                    u => u.FindElement(By.XPath(string.Format(_xpathCmb, "SeatType", ticket.SeatType)
                    )));
                new SelectElement(CmbSeatType).SelectByText(ticket.SeatType);
            }
            new SelectElement(CmbTicketAmount).SelectByText(ticket.Amount);

            //submit
            BtnBookTicket.Click();

            return new BookTicketSuccessPage();
        }
        public string GetDepartFromSelectedValue()
        {
            return CmbDepartFromSelected.Text;
        }
        public string GetArriveAtSelectedValue()
        {
            return CmbArriveAtSelected.Text;
        }
        public string GetSeatTypeSelectedValue()
        {
            return CmbSeatTypeSelected.Text;
        }

        #endregion
    }
}
