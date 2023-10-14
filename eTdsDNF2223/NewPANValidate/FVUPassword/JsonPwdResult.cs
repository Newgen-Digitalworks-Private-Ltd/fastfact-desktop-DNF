using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.FVUPassword
{
    public class JsonPwdResult
    {
        public headerresult header = null;
        public List<messagesresult> messages = null;
        public List<object>  errors { get; set; }
        public string reqId { get; set; }
        public string entity { get; set; }
        public string entityType { get; set; }
        public string role { get; set; }
        public string userType { get; set; }
        public string uidValdtnFlg { get; set; }
        public string passValdtnFlg { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
        public string aadhaarMobileValidated { get; set; }
        public string secAccssMsg { get; set; }
        public string secLoginOptions { get; set; }
        public string aadhaarLinkedWithUserId { get; set; }
        public string contactPan { get; set; }
        public string contactEmail { get; set; }
        public string contactMobile { get; set; }
        public string lastLoginSuccessFlag { get; set; }
        public string clientIp { get; set; }
        public string exemptedPan { get; set; }
        public string userConsent { get; set; }
        public string imgByte { get; set; }
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
