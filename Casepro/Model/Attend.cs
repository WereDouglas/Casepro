using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Attend
    {
        private string _attendID;
        private string _orgID;
        private string _taskID;
        private string _userID;
        private string _name;
        private string _contact;
        private string _email;

        public Attend(string _attendID, string _orgID, string _taskID, string _userID, string _name, string _contact, string _email)
        {
            this._attendID = _attendID;
            this._orgID = _orgID;
            this._taskID = _taskID;
            this._userID = _userID;
            this._name = _name;
            this._contact = _contact;
            this._email = _email;
        }

        public string AttendID
        {
            get
            {
                return _attendID;
            }

            set
            {
                _attendID = value;
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

        public string TaskID
        {
            get
            {
                return _taskID;
            }

            set
            {
                _taskID = value;
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

        public string Contact
        {
            get
            {
                return _contact;
            }

            set
            {
                _contact = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }
    }
}
