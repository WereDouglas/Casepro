using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
  public  class Expense
    {
        private string _expenseID;
        private string _orgID;
        private string _clientID;
        private string _fileID;
        private string _details;
        private string _lawyer;
        private string _method;
        private string _amount;
        private string _no;
        private string _balance;
        private string _paid;
        private string _date;
        private string _approved;
        private string _signed;
        private string _reason;
        private string _outcome;
        private string _deadline;

        public Expense(string _expenseID, string _orgID, string _clientID, string _fileID, string _details, string _lawyer, string _method, string _amount, string _no, string _balance, string _paid, string _date, string _approved, string _signed, string _reason, string _outcome, string _deadline)
        {
            this._expenseID = _expenseID;
            this._orgID = _orgID;
            this._clientID = _clientID;
            this._fileID = _fileID;
            this._details = _details;
            this._lawyer = _lawyer;
            this._method = _method;
            this._amount = _amount;
            this._no = _no;
            this._balance = _balance;
            this._paid = _paid;
            this._date = _date;
            this._approved = _approved;
            this._signed = _signed;
            this._reason = _reason;
            this._outcome = _outcome;
            this._deadline = _deadline;
        }

        public string ExpenseID
        {
            get
            {
                return _expenseID;
            }

            set
            {
                _expenseID = value;
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

        public string Reason
        {
            get
            {
                return _reason;
            }

            set
            {
                _reason = value;
            }
        }

        public string Outcome
        {
            get
            {
                return _outcome;
            }

            set
            {
                _outcome = value;
            }
        }

        public string Deadline
        {
            get
            {
                return _deadline;
            }

            set
            {
                _deadline = value;
            }
        }
    }
}
