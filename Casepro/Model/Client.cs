using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
  public class Client
    {

      

        public Client()
        {
           
        }

        private string _clientID;
        private string _orgID;
        private string _name;
        private string _email;
        private string _password;
        private string _lawyer;
        private string _status;
        private string _contact;      
        private string _image;
        private string _imageUrl;
        private string _address;
        private string _registration;
        private string _created;
        private string _sync;

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

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
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

        public string Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }

            set
            {
                _imageUrl = value;
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }

        public string Registration
        {
            get
            {
                return _registration;
            }

            set
            {
                _registration = value;
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
    }

}
