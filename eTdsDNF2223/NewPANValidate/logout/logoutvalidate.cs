using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;
using System.Net;
using NewPANValidate.FVUPassword;

namespace NewPANValidate.logout
{
    public class logoutvalidate
    {
        WebClient oWebClient;
        public logoutvalidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(FVUPassword.JsonPwdResult oJsonPwdResult)
        {
            try
            {
                logoutrequest ologoutrequest = new logoutrequest();
                ologoutrequest.serviceName = "logoutService";
                ologoutrequest.entity = oJsonPwdResult.entity;
                ologoutrequest.userType = oJsonPwdResult.role;

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(ologoutrequest.GetType(), ologoutrequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/loginapi/login", "POST", oReturnStatus.pro_description);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
