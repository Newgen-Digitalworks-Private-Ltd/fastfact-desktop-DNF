using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.logout
{
    public class logoutrequest
    {
        private string _entity = string.Empty;
        public String entity
        {
            get { return _entity; }
            set { _entity = value; }
        }
        private string _serviceName = string.Empty;
        public String serviceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
        private string _userType = string.Empty;
        public String userType
        {
            get { return _userType; }
            set { _userType = value; }
        }
    }
}
