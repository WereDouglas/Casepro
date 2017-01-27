using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casepro.Model
{
   public class Rule
    {
        private string _ruleID;
        private string _name;
        private string _period;
        private string _orgID;

        public Rule(string _ruleID, string _name, string _period, string _orgID)
        {
            this._ruleID = _ruleID;
            this._name = _name;
            this._period = _period;
            this._orgID = _orgID;
        }

        public string RuleID
        {
            get
            {
                return _ruleID;
            }

            set
            {
                _ruleID = value;
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
    }
}
