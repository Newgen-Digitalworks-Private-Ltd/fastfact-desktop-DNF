using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.fileincometaxupload
{
    public class jsonfileincometaxuploadresult
    {
        public string catACnt { get; set; }
        public string catBCnt { get; set; }
        public string catCCnt { get; set; }
        public string submittedCnt { get; set; }
        public List<object> inPrgrsFrms = null;
        public List<cateCFrmsresult> cateCFrms = null;
        public string entityNum { get; set; }
        public string roleCd { get; set; }
    }
    public class cateCFrmsresult
    {
        public string formName { get; set; }
        public string formShortName { get; set; }
        public string formDesc { get; set; }
        public string formCd { get; set; }
        public string refYear { get; set; }
        public string chapterDesc { get; set; }
        public List<object> sectionCd = null;
        public List<object> appFinYrArr = null;
        public List<object> fileTypeArr = null;
        public List<object> ruleCd = null;
        public string appYrType { get; set; }
        public string appQrtr { get; set; }
        public string appFreq { get; set; }
        public string appMode { get; set; }
    }
}
