using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;
using System.Net;
using NewPANValidate.FVUUpload;
namespace NewPANValidate.FVUPassword
{
    public class PwdValidate
    {
        public JsonPwdResult oJsonResult = null;
        WebClient oWebClient;
        public PwdValidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(JsonUserResult oJsonUserResult,string pwd,out string authtoken)
        {
            authtoken = string.Empty;
            try
            {
                PwdRequest oPwdRequest = new PwdRequest();
                oPwdRequest.aadhaarLinkedWithUserId = oJsonUserResult.aadhaarLinkedWithUserId;
                oPwdRequest.aadhaarMobileValidated= oJsonUserResult.aadhaarMobileValidated;
                oPwdRequest.contactEmail= null;
                oPwdRequest.contactMobile= null;
                oPwdRequest.contactPan= null;
                oPwdRequest.email= null;
                oPwdRequest.entity= oJsonUserResult.entity;
                oPwdRequest.entityType= oJsonUserResult.entityType;
                oPwdRequest.errors= oJsonUserResult.errors;
                oPwdRequest.exemptedPan= oJsonUserResult.exemptedPan;
                oPwdRequest.forgnDirEmailId= null;
                oPwdRequest.imgByte= oJsonUserResult.imgByte;
                oPwdRequest.mobileNo= null;
                oPwdRequest.otp= null;
                oPwdRequest.otpGenerationFlag= null;
                oPwdRequest.otpSourceFlag= null;
                oPwdRequest.otpValdtnFlg= null;
                oPwdRequest.pass = Convert.ToBase64String(Encoding.UTF8.GetBytes(pwd));
                oPwdRequest.passValdtnFlg= null;
                oPwdRequest.reqId= oJsonUserResult.reqId;
                oPwdRequest.role= oJsonUserResult.role;
                oPwdRequest.secAccssMsg= oJsonUserResult.secAccssMsg;
                oPwdRequest.secLoginOptions= oJsonUserResult.secLoginOptions;
                oPwdRequest.serviceName= "loginService";
                oPwdRequest.uidValdtnFlg= oJsonUserResult.uidValdtnFlg;
                oPwdRequest.userConsent= oJsonUserResult.userConsent;

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(oPwdRequest.GetType(), oPwdRequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/loginapi/login", "POST", oReturnStatus.pro_description);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                oJsonResult = new JsonPwdResult();
                authtoken = (string)oReturnStatus.pro_object;
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (JsonPwdResult)oReturnStatus.pro_object;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
