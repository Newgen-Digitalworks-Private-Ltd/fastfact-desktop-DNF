using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate
{
    public class PANRequest
    {
        private string _serviceName = string.Empty;
        public String serviceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }

        private string _userId = string.Empty;
        public String userId
        {
            get { return _userId; }
            set { _userId = value; }
        }

    }
}
