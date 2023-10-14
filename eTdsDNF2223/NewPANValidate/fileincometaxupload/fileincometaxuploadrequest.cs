using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.fileincometaxupload
{
    public class fileincometaxuploadrequest
    {
        private string _entityNum = string.Empty;
        public String entityNum
        {
            get { return _entityNum; }
            set { _entityNum = value; }
        }
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
}
