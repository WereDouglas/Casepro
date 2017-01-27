using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Item
    {
        private string _itemID;
        private string _orgID;
        private string _transID;
        private string _name;
        private string _description;
        private string _rate;
        private string _qty;
        private string _total;

        public Item(string _itemID, string _orgID, string _transID, string _name, string _description, string _rate, string _qty, string _total)
        {
            this._itemID = _itemID;
            this._orgID = _orgID;
            this._transID = _transID;
            this._name = _name;
            this._description = _description;
            this._rate = _rate;
            this._qty = _qty;
            this._total = _total;
        }

        public string ItemID
        {
            get
            {
                return _itemID;
            }

            set
            {
                _itemID = value;
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

        public string TransID
        {
            get
            {
                return _transID;
            }

            set
            {
                _transID = value;
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

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public string Rate
        {
            get
            {
                return _rate;
            }

            set
            {
                _rate = value;
            }
        }

        public string Qty
        {
            get
            {
                return _qty;
            }

            set
            {
                _qty = value;
            }
        }

        public string Total
        {
            get
            {
                return _total;
            }

            set
            {
                _total = value;
            }
        }
    }
}
