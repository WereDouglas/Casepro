using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Disbursement
    {
        private string _disbursementID;
        private string _orgID;
        private string _clientID;
        private string _fileID;
        private string _details;
        private string _lawyer;
        private string _paid;
        private string _invoice;
        private string _method;
        private string _amount;
        private string _received;
        private string _balance;
        private string _approved;
        private string _signed;
        private string _date;

        public Disbursement(string _disbursementID, string _orgID, string _clientID, string _fileID, string _details, string _lawyer, string _paid, string _invoice, string _method, string _amount, string _received, string _balance, string _approved, string _signed, string _date)
        {
            this._disbursementID = _disbursementID;
            this._orgID = _orgID;
            this._clientID = _clientID;
            this._fileID = _fileID;
            this._details = _details;
            this._lawyer = _lawyer;
            this._paid = _paid;
            this._invoice = _invoice;
            this._method = _method;
            this._amount = _amount;
            this._received = _received;
            this._balance = _balance;
            this._approved = _approved;
            this._signed = _signed;
            this._date = _date;
        }

        public string DisbursementID
        {
            get
            {
                return _disbursementID;
            }

            set
            {
                _disbursementID = value;
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

        public string ClientID
        {
            get
            {
                return _clientID;
            }

            set
            {
                _clientID = value;
            }
        }

        public string FileID
        {
            get
            {
                return _fileID;
            }

            set
            {
                _fileID = value;
            }
        }

        public string Details
        {
            get
            {
                return _details;
            }

            set
            {
                _details = value;
            }
        }

        public string Lawyer
        {
            get
            {
                return _lawyer;
            }

            set
            {
                _lawyer = value;
            }
        }

        public string Paid
        {
            get
            {
                return _paid;
            }

            set
            {
                _paid = value;
            }
        }

        public string Invoice
        {
            get
            {
                return _invoice;
            }

            set
            {
                _invoice = value;
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

        public string Received
        {
            get
            {
                return _received;
            }

            set
            {
                _received = value;
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

        public string Signed
        {
            get
            {
                return _signed;
            }

            set
            {
                _signed = value;
            }
        }

        public string Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }
    }
}
