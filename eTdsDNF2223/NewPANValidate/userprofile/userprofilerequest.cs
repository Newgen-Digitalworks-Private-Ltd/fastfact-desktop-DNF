using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.userprofile
{
    public class userprofilerequest
    {
        private string _serviceName = string.Empty;
        public String serviceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
    }
}
