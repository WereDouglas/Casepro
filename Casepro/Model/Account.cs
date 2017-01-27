using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Account
    {
        private string _accountID;
        private string _orgID;
        private string _name;
        private string _bank;
        private string _no;
        private string _userID;
        private string _sync;
        private string _clientID;

        public string AccountID
        {
            get
            {
                return _accountID;
            }

            set
            {
                _accountID = value;
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

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Bank
        {
            get
            {
                return _bank;
            }

            set
            {
                _bank = value;
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

        public string Sync
        {
            get
            {
                return _sync;
            }

            set
            {
                _sync = value;
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

        public Account(string _accountID, string _orgID, string _name, string _bank, string _no, string _userID, string _sync, string _clientID)
        {
            this.AccountID = _accountID;
            this.OrgID = _orgID;
            this.Name = _name;
            this.Bank = _bank;
            this.No = _no;
            this.UserID = _userID;
            this.Sync = _sync;
            this.ClientID = _clientID;
        }
    }
}
