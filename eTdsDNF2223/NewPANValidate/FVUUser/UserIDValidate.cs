using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;
using System.Net;

namespace NewPANValidate.FVUUpload
{
    public class UserIDValidate
    {
        public JsonUserResult oJsonResult = null;
        WebClient oWebClient;
        public UserIDValidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(string userid)
        {
            try
            {
                UserIDRequest oUserIDRequest = new UserIDRequest();
                oUserIDRequest.serviceName = "loginService";
                oUserIDRequest.entity  = userid;

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(oUserIDRequest.GetType(), oUserIDRequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/loginapi/login", "POST", oReturnStatus.pro_description);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                oJsonResult = new JsonUserResult();
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (JsonUserResult)oReturnStatus.pro_object;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
