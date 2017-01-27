using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
  public  class Document
    {
        private string _documentID;
        private string _orgID;
        private string _fileID;
        private string _name;
        private string _client;
        private string _details;
        private string _fileUrl;
        private string _created;

        public Document(string _documentID, string _orgID, string _fileID, string _name, string _client, string _details, string _fileUrl, string _created)
        {
            this._documentID = _documentID;
            this._orgID = _orgID;
            this._fileID = _fileID;
            this._name = _name;
            this._client = _client;
            this._details = _details;
            this._fileUrl = _fileUrl;
            this._created = _created;
        }

        public string DocumentID
        {
            get
            {
                return _documentID;
            }

            set
            {
                _documentID = value;
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

        public string FileUrl
        {
            get
            {
                return _fileUrl;
            }

            set
            {
                _fileUrl = value;
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
