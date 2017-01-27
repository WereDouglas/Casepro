using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Organisation
    {
        private string _orgID;
        private string _name;
        private string _license;
        private string _starts;
        private string _ends;
        private string _code;
        private string _address;
        private string _email;
        private string _status;
        private string _image;
        private string _currency;
        private string _country;
        private string _region;
        private string _city;

        public Organisation(string _orgID, string _name, string _license, string _starts, string _ends, string _code, string _address, string _email, string _status, string _image, string _currency, string _country, string _region, string _city)
        {
            this._orgID = _orgID;
            this._name = _name;
            this._license = _license;
            this._starts = _starts;
            this._ends = _ends;
            this._code = _code;
            this._address = _address;
            this._email = _email;
            this._status = _status;
            this._image = _image;
            this._currency = _currency;
            this._country = _country;
            this._region = _region;
            this._city = _city;
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

        public string License
        {
            get
            {
                return _license;
            }

            set
            {
                _license = value;
            }
        }

        public string Starts
        {
            get
            {
                return _starts;
            }

            set
            {
                _starts = value;
            }
        }

        public string Ends
        {
            get
            {
                return _ends;
            }

            set
            {
                _ends = value;
            }
        }

        public string Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
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

        public string Currency
        {
            get
            {
                return _currency;
            }

            set
            {
                _currency = value;
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }

            set
            {
                _country = value;
            }
        }

        public string Region
        {
            get
            {
                return _region;
            }

            set
            {
                _region = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }
    }
}
