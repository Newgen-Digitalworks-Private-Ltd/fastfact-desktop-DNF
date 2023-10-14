using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate.PANAadharStatus
{
    public class PANAadharRequest
    {
        private string _aadhaarNumber = string.Empty;
        public String aadhaarNumber
        {
            get { return _aadhaarNumber; }
            set { _aadhaarNumber = value; }
        }

        private string _pan = string.Empty;
        public String pan
        {
            get { return _pan; }
            set { _pan = value; }
        }

        private string _preLoginFlag = string.Empty;
        public String preLoginFlag
        {
            get { return _preLoginFlag; }
            set { _preLoginFlag = value; }
        }

        private string _serviceName = string.Empty;
        public String serviceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
    }
}
