using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPANValidate.FVUPassword
{
    public class PwdRequest
    {
        private string _aadhaarLinkedWithUserId = string.Empty;
        public String aadhaarLinkedWithUserId
        {
            get { return _aadhaarLinkedWithUserId; }
            set { _aadhaarLinkedWithUserId = value; }
        }
        private string _aadhaarMobileValidated = string.Empty;
        public String aadhaarMobileValidated
        {
            get { return _aadhaarMobileValidated; }
            set { _aadhaarMobileValidated = value; }
        }
        private string _contactEmail = string.Empty;
        public String contactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; }
        }
        private string _contactMobile = string.Empty;
        public String contactMobile
        {
            get { return _contactMobile; }
            set { _contactMobile = value; }
        }
        private string _contactPan = string.Empty;
        public String contactPan
        {
            get { return _contactPan; }
            set { _contactPan = value; }
        }
        private string _email = string.Empty;
        public String email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _entity = string.Empty;
        public String entity
        {
            get { return _entity; }
            set { _entity = value; }
        }
        private string _entityType = string.Empty;
        public String entityType
        {
            get { return _entityType; }
            set { _entityType = value; }
        }
        private List<object> _errors = null;
        public List<object> errors
        {
            get { return _errors; }
            set { _errors = value; }
        }
        private string _exemptedPan = string.Empty;
        public String exemptedPan
        {
            get { return _exemptedPan; }
            set { _exemptedPan = value; }
        }
        private string _forgnDirEmailId = string.Empty;
        public String forgnDirEmailId
        {
            get { return _forgnDirEmailId; }
            set { _forgnDirEmailId = value; }
        }
        private string _imgByte = string.Empty;
        public String imgByte
        {
            get { return _imgByte; }
            set { _imgByte = value; }
        }
        private string _mobileNo = string.Empty;
        public String mobileNo
        {
            get { return _mobileNo; }
            set { _mobileNo = value; }
        }
        private string _otp = string.Empty;
        public String otp
        {
            get { return _otp; }
            set { _otp = value; }
        }
        private string _otpGenerationFlag = string.Empty;
        public String otpGenerationFlag
        {
            get { return _otpGenerationFlag; }
            set { _otpGenerationFlag = value; }
        }
        private string _otpSourceFlag = string.Empty;
        public String otpSourceFlag
        {
            get { return _otpSourceFlag; }
            set { _otpSourceFlag = value; }
        }
        private string _otpValdtnFlg = string.Empty;
        public String otpValdtnFlg
        {
            get { return _otpValdtnFlg; }
            set { _otpValdtnFlg = value; }
        }
        private string _pass = string.Empty;
        public String pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        private string _passValdtnFlg = string.Empty;
        public String passValdtnFlg
        {
            get { return _passValdtnFlg; }
            set { _passValdtnFlg = value; }
        }
        private string _reqId = string.Empty;
        public String reqId
        {
            get { return _reqId; }
            set { _reqId = value; }
        }
        private string _role = string.Empty;
        public String role
        {
            get { return _role; }
            set { _role = value; }
        }
        private string _secAccssMsg = string.Empty;
        public String secAccssMsg
        {
            get { return _secAccssMsg; }
            set { _secAccssMsg = value; }
        }
        private string _secLoginOptions = string.Empty;
        public String secLoginOptions
        {
            get { return _secLoginOptions; }
            set { _secLoginOptions = value; }
        }
        private string _serviceName = string.Empty;
        public String serviceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
        private string _uidValdtnFlg = string.Empty;
        public String uidValdtnFlg
        {
            get { return _uidValdtnFlg; }
            set { _uidValdtnFlg = value; }
        }
        private string _userConsent = string.Empty;
        public String userConsent
        {
            get { return _userConsent; }
            set { _userConsent = value; }
        }
    }
}
