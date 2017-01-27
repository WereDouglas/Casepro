using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
    class Task
    {
        private string _taskID;
        private string _orgID;
        private string _details;
        private string _startTime;
        private string _endTime;
        private string _date;
        private string _trigger;
        private string _period;
        private string _priority;
        private string _userID;
        private string _fileID;
        private string _created;

        public Task(string _taskID, string _orgID, string _details, string _startTime, string _endTime, string _date, string _trigger, string _period, string _priority, string _userID, string _fileID, string _created)
        {
            this._taskID = _taskID;
            this._orgID = _orgID;
            this._details = _details;
            this._startTime = _startTime;
            this._endTime = _endTime;
            this._date = _date;
            this._trigger = _trigger;
            this._period = _period;
            this._priority = _priority;
            this._userID = _userID;
            this._fileID = _fileID;
            this._created = _created;
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

        public string StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value;
            }
        }

        public string EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
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

        public string Trigger
        {
            get
            {
                return _trigger;
            }

            set
            {
                _trigger = value;
            }
        }

        public string Period
        {
            get
            {
                return _period;
            }

            set
            {
                _period = value;
            }
        }

        public string Priority
        {
            get
            {
                return _priority;
            }

            set
            {
                _priority = value;
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
    }
}
