using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Payment
    {
        private string _paymentID;
        private string _orgID;
        private string _transID;
        private string _amount;
        private string _method;
        private string _no;
        private string _userID;
        private string _status;
        private string _recieved;
        private string _balance;
        private string _created;
        private string _approved;

        public Payment(string _paymentID, string _orgID, string _transID, string _amount, string _method, string _no, string _userID, string _status, string _recieved, string _balance, string _created, string _approved)
        {
            this._paymentID = _paymentID;
            this._orgID = _orgID;
            this._transID = _transID;
            this._amount = _amount;
            this._method = _method;
            this._no = _no;
            this._userID = _userID;
            this._status = _status;
            this._recieved = _recieved;
            this._balance = _balance;
            this._created = _created;
            this._approved = _approved;
        }

        public string PaymentID
        {
            get
            {
                return _paymentID;
            }

            set
            {
                _paymentID = value;
            }
        }

        public string OrgID
        {
            get
            {
                return _orgID;
            }

            set
            {
                _orgID = value;
            }
        }

        public string TransID
        {
            get
            {
                return _transID;
            }

            set
            {
                _transID = value;
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

        public string Method
        {
            get
            {
                return _method;
            }

            set
            {
                _method = value;
            }
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

        public string UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                _userID = value;
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

        public string Recieved
        {
            get
            {
                return _recieved;
            }

            set
            {
                _recieved = value;
            }
        }

        public string Balance
        {
            get
            {
                return _balance;
            }

            set
            {
                _balance = value;
            }
        }

        public string Created
        {
            get
            {
                return _created;
            }

            set
            {
                _created = value;
            }
        }

        public string Approved
        {
            get
            {
                return _approved;
            }

            set
            {
                _approved = value;
            }
        }
    }
}
