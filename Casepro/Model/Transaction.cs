using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
    public class Transaction
    {
        private string _transID;
        private string _orgID;
        private string _staff;
        private string _client;
        private string _type;
        private string _created;
        private string _fileID;
        private string _status;
        private string _total;
        private string _category;
        private string _details;
        private string _invoice;
        private string _no;
        private string _sub;
        private string _vat;
        private string _balance;
        private string _dueDate;
        private string _method;
        private string _paid;

        public Transaction(string _transID, string _orgID, string _staff, string _client, string _type, string _created, string _fileID, string _status, string _total, string _category, string _details, string _invoice, string _no, string _sub, string _vat, string _balance, string _dueDate, string _method, string _paid)
        {
            this._transID = _transID;
            this._orgID = _orgID;
            this._staff = _staff;
            this._client = _client;
            this._type = _type;
            this._created = _created;
            this._fileID = _fileID;
            this._status = _status;
            this._total = _total;
            this._category = _category;
            this._details = _details;
            this._invoice = _invoice;
            this._no = _no;
            this._sub = _sub;
            this._vat = _vat;
            this._balance = _balance;
            this._dueDate = _dueDate;
            this._method = _method;
            this._paid = _paid;
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

        public string Staff
        {
            get
            {
                return _staff;
            }

            set
            {
                _staff = value;
            }
        }

        public string Client
        {
            get
            {
                return _client;
            }

            set
            {
                _client = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
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

        public string Total
        {
            get
            {
                return _total;
            }

            set
            {
                _total = value;
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }

            set
            {
                _category = value;
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

        public string Sub
        {
            get
            {
                return _sub;
            }

            set
            {
                _sub = value;
            }
        }

        public string Vat
        {
            get
            {
                return _vat;
            }

            set
            {
                _vat = value;
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

        public string DueDate
        {
            get
            {
                return _dueDate;
            }

            set
            {
                _dueDate = value;
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
    }
}
