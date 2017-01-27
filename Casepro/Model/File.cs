using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
    public class File
    {
        public File()

        {


        }
        private string _fileID;
        private string _orgID;
        private string _client;
        private string _lawyer;
        private string _no;
        private string _details;
        private string _type;
        private string _subject;
        private string _citation;
        private string _law;
        private string _name;
        private string _created;
        private string _status;

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
        private string _action;

        public string Action
        {
            get { return _action; }
            set { _action = value; }
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

        public string Citation
        {
            get
            {
                return _citation;
            }

            set
            {
                _citation = value;
            }
        }

        public string Law
        {
            get
            {
                return _law;
            }

            set
            {
                _law = value;
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




    }
}