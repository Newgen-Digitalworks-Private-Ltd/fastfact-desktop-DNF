using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate
{
    public class JsonResult
    {
        public headerresult header = null;
        public List<messagesresult> messages = null;
        public List<object>  errors { get; set; }
        public string userId { get; set; }
        public string status { get; set; }
        public string isMigrated { get; set; }
    }
    public class headerresult
    {
        public string formName { get; set; }
    }
    public class messagesresult
    {
        public string code { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
        public string fieldName { get; set; }
    }

}
