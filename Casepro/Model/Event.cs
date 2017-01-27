using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Event
    {
        private string _id;
        private string _name;
        private string _start;
        private string _end;
        private string _user;
        private string _file;
        private string _created;
        private string _sync;
        private string _status;
        private string _orgID;
        private string _date;
        private string _hours;

        public Event(string _id, string _name, string _start, string _end, string _user, string _file, string _created, string _sync, string _status, string _orgID, string _date, string _hours)
        {
            this._id = _id;
            this._name = _name;
            this._start = _start;
            this._end = _end;
            this._user = _user;
            this._file = _file;
            this._created = _created;
            this._sync = _sync;
            this._status = _status;
            this._orgID = _orgID;
            this._date = _date;
            this._hours = _hours;
        }

        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
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

        public string Start
        {
            get
            {
                return _start;
            }

            set
            {
                _start = value;
            }
        }

        public string End
        {
            get
            {
                return _end;
            }

            set
            {
                _end = value;
            }
        }

        public string User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
            }
        }

        public string File
        {
            get
            {
                return _file;
            }

            set
            {
                _file = value;
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

        public string Hours
        {
            get
            {
                return _hours;
            }

            set
            {
                _hours = value;
            }
        }
    }
}
