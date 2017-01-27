using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Note
    {
        private string _noteID;
        private string _fileID;
        private string _orgID;
        private string _userID;
        private string _content;
        private string _created;

        public Note(string _noteID, string _fileID, string _orgID, string _userID, string _content, string _created)
        {
            this._noteID = _noteID;
            this._fileID = _fileID;
            this._orgID = _orgID;
            this._userID = _userID;
            this._content = _content;
            this._created = _created;
        }

        public string NoteID
        {
            get
            {
                return _noteID;
            }

            set
            {
                _noteID = value;
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

        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
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
