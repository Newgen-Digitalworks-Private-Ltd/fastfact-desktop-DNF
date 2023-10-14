using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.dashboard
{
    public class dashboardrequest
    {
        public headerrequest header = null;
        private string _roleCd = string.Empty;
        public String roleCd
        {
            get { return _roleCd; }
            set { _roleCd = value; }
        }
        private string _serviceName = string.Empty;
        public String serviceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
    }
    public class headerrequest
    {
        private string _formname { get; set; }
        public String formname
        {
            get { return _formname; }
            set { _formname = value; }
        }
    }
}
