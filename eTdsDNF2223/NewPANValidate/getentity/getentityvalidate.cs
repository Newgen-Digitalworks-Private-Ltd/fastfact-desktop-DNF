using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate.getentity
{
    public class getentityvalidate
    {
        public jsongetentityresult oJsonResult = null;
        WebClient oWebClient;
        public getentityvalidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(string authToken)
        {
            try
            {
                getentityrequest ogetentityrequest = new getentityrequest();
                ogetentityrequest.serviceName = "verifyEVCUsingNetBanking";

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(ogetentityrequest.GetType(), ogetentityrequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/verificationservices/auth/getEntity", "POST", oReturnStatus.pro_description, authToken);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = new jsongetentityresult();
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (jsongetentityresult)oReturnStatus.pro_object;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
