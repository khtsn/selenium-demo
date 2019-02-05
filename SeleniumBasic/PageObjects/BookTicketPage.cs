using SeleniumBasic.Common;

using OpenQA.Selenium;

namespace SeleniumBasic.PageObjects
{
    class BookTicketPage
    {
        #region Locators
        static readonly By _cmbDepartDate = By.XPath("//select[@name='date']");
        static readonly By _cmbDepartFrom = By.XPath("//select[@name='DepartStation']");
        static readonly By _cmbArriveAt = By.XPath("//select[@name='ArriveStation']");
        static readonly By _cmbSeatType = By.XPath("//select[@name='SeatType']");
        static readonly By _cmbTicketAmount = By.XPath("//select[@name='TicketAmount']");

        static readonly By _btnBookTicket = By.XPath("//input[@type='submit' and @value='Book ticket']");
        #endregion

        #region Elements
        #endregion

        #region Methods
        #endregion
    }
}
