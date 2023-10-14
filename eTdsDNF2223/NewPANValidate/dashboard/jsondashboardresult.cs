using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.dashboard
{
    public class jsondashboardresult
    {
        public string server { get; set; }
        public List<childrenresult> children = null;
        public string newPage { get; set; }
        public string menuType { get; set; }
        public string label { get; set; }
        public object url { get; set; }
    }
    public class childrenresult
    {
        public string server { get; set; }
        public List<childrenresult> children = null;
        public string newPage { get; set; }
        public string menuType { get; set; }
        public string label { get; set; }
        public object url { get; set; }
    }
}
