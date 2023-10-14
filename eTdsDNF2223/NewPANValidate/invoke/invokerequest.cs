using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.invoke
{
    public class invokerequest
    {
        public metadatarequest metadata = null;
        public datarequest data = null;
    }
    public class metadatarequest
    {
        private string _entityType = string.Empty;
        public String entityType
        {
            get { return _entityType; }
            set { _entityType = value; }
        }
        private string _filingType = string.Empty;
        public String filingType
        {
            get { return _filingType; }
            set { _filingType = value; }
        }
        private string _refYearType = string.Empty;
        public String refYearType
        {
            get { return _refYearType; }
            set { _refYearType = value; }
        }
        private string _financialQtr = string.Empty;
        public String financialQtr
        {
            get { return _financialQtr; }
            set { _financialQtr = value; }
        }
        private string _refYear = string.Empty;
        public String refYear
        {
            get { return _refYear; }
            set { _refYear = value; }
        }
        private string _operation = string.Empty;
        public String operation
        {
            get { return _operation; }
            set { _operation = value; }
        }
        private string _formName = string.Empty;
        public String formName
        {
            get { return _formName; }
            set { _formName = value; }
        }
        private string _sn = string.Empty;
        public String sn
        {
            get { return _sn; }
            set { _sn = value; }
        }
    }
    public class datarequest
    {
        private string _entityNumber = string.Empty;
        public String entityNumber
        {
            get { return _entityNumber; }
            set { _entityNumber = value; }
        }
    }
}
