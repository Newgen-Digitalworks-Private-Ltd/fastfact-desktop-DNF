using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate
{
    public class PANValidate
    {
        public JsonResult oJsonResult = null;
        public bool Validate(string URL,string PAN)
        {
            try
            {
                PANRequest oPANRequest = new PANRequest();
                if (PAN.Substring(3, 1).ToString() == "P") oPANRequest.serviceName = "checkPanDetailsService";
                else oPANRequest.serviceName = "checkCorporateDetailsService"; 
                oPANRequest.userId = PAN;

                Json oJson = new Json();
                oReturnStatus = oJson.JsonStringFormat(oPANRequest.GetType(), oPANRequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                //ServicePointManager.Expect100Continue = true;
                //if (SecurityProtocolType.Ssl3.ToString() == "Ssl3") ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                //else if (SecurityProtocolType.Ssl3.ToString() == "Tls") ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                //else if (SecurityProtocolType.Ssl3.ToString() == "Tls11") ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
                //else if (SecurityProtocolType.Ssl3.ToString() == "Tls12") ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //else ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request(URL, "POST", oReturnStatus.pro_description);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                oJsonResult = new JsonResult();
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (JsonResult)oReturnStatus.pro_object;

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
