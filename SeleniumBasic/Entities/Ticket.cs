using OpenQA.Selenium;
using System.Linq;

namespace SeleniumBasic.Entities
{
    public class Ticket
    {
        private string _no;
        private string _departStation;
        private string _arriveStation;
        private string _seatType;
        private string _departDate;
        private string _bookDate;
        private string _expiredDate;
        private string _status;
        private string _amount;
        private string _totalPrice;
        private IWebElement _operation;

        private string _departTime;
        private string _arriveTime;
        private IWebElement _checkPrice;
        private IWebElement _bookTicket;

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        public string No
        {
            get
            {
                return _no;
            }

            set
            {
                _no = value;
            }
        }

        public string DepartStation
        {
            get
            {
                return _departStation;
            }

            set
            {
                _departStation = value;
            }
        }

        public string ArriveStation
        {
            get
            {
                return _arriveStation;
            }

            set
            {
                _arriveStation = value;
            }
        }

        public string SeatType
        {
            get
            {
                return _seatType;
            }

            set
            {
                _seatType = value;
            }
        }

        public string DepartDate
        {
            get
            {
                return _departDate;
            }

            set
            {
                _departDate = value;
            }
        }

        public string BookDate
        {
            get
            {
                return _bookDate;
            }

            set
            {
                _bookDate = value;
            }
        }

        public string ExpiredDate
        {
            get
            {
                return _expiredDate;
            }

            set
            {
                _expiredDate = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        public string Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;
            }
        }

        public string TotalPrice
        {
            get
            {
                return _totalPrice;
            }

            set
            {
                _totalPrice = value;
            }
        }

        public IWebElement Operation
        {
            get
            {
                return _operation;
            }

            set
            {
                _operation = value;
            }
        }

        public IWebElement CheckPrice
        {
            get
            {
                return _checkPrice;
            }

            set
            {
                _checkPrice = value;
            }
        }

        public IWebElement BookTicket
        {
            get
            {
                return _bookTicket;
            }

            set
            {
                _bookTicket = value;
            }
        }

        public string DepartTime
        {
            get
            {
                return _departTime;
            }

            set
            {
                _departTime = value;
            }
        }

        public string ArriveTime
        {
            get
            {
                return _arriveTime;
            }

            set
            {
                _arriveTime = value;
            }
        }
    }
}
