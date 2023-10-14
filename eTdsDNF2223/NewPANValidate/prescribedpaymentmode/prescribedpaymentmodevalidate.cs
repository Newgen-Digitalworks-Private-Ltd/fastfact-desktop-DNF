using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate.prescribedpaymentmode
{
    public class prescribedpaymentmodevalidate
    {
        public jsonprescribedpaymentmoderesult oJsonResult;
        WebClient oWebClient;
        public prescribedpaymentmodevalidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(string authtoken)
        {
            try
            {
                prescribedpaymentmoderequest oprescribedpaymentmoderequest = new prescribedpaymentmoderequest();
                oprescribedpaymentmoderequest.serviceName = "prescribedPaymentModeService";

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(oprescribedpaymentmoderequest.GetType(), oprescribedpaymentmoderequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/servicesapi/auth/saveEntity", "POST", oReturnStatus.pro_description, authtoken);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                oJsonResult = new jsonprescribedpaymentmoderesult();
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (jsonprescribedpaymentmoderesult)oReturnStatus.pro_object;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
