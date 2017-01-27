using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Message
    {
        private string _messageID;
        private string _body;
        private string _subject;
        private string _to;
        private string _from;
        private string _date;
        private string _created;
        private string _orgID;
        private string _sent;
        private string _type;
        private string _contact;
        private string _email;
        private string _taskID;

        public Message(string _messageID, string _body, string _subject, string _to, string _from, string _date, string _created, string _orgID, string _sent, string _type, string _contact, string _email, string _taskID)
        {
            this._messageID = _messageID;
            this._body = _body;
            this._subject = _subject;
            this._to = _to;
            this._from = _from;
            this._date = _date;
            this._created = _created;
            this._orgID = _orgID;
            this._sent = _sent;
            this._type = _type;
            this._contact = _contact;
            this._email = _email;
            this._taskID = _taskID;
        }

        public string MessageID
        {
            get
            {
                return _messageID;
            }

            set
            {
                _messageID = value;
            }
        }

        public string Body
        {
            get
            {
                return _body;
            }

            set
            {
                _body = value;
            }
        }

        public string Subject
        {
            get
            {
                return _subject;
            }

            set
            {
                _subject = value;
            }
        }

        public string To
        {
            get
            {
                return _to;
            }

            set
            {
                _to = value;
            }
        }

        public string From
        {
            get
            {
                return _from;
            }

            set
            {
                _from = value;
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

        public string Sent
        {
            get
            {
                return _sent;
            }

            set
            {
                _sent = value;
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
    }
}
