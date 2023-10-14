using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.getentity
{
    public class jsongetentityresult
    {
        public headerresult header = null;
        public List<object> messages { get; set; }
        public List<object> errors { get; set; }
        public string panNumber { get; set; }
    }
    public class headerresult
    {
        public string formName { get; set; }
    }
}
