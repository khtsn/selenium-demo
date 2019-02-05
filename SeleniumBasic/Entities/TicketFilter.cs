using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBasic.Entities
{
    public class TicketFilter
    {
        private string _departStation;

        public string DepartStation
        {
            get { return _departStation; }
            set { _departStation = value; }
        }
        private string _arriveStation;

        public string ArriveStation
        {
            get { return _arriveStation; }
            set { _arriveStation = value; }
        }
        private string _departDate;

        public string DepartDate
        {
            get { return _departDate; }
            set { _departDate = value; }
        }
        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
