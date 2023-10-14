using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;
using System.Net;
using NewPANValidate.FVUUpload;
using NewPANValidate.FVUPassword;

namespace NewPANValidate.dashboard
{
    public class dashboardvalidate
    {
        public List<jsondashboardresult> oJsonResult;
        WebClient oWebClient;
        public dashboardvalidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(FVUPassword.JsonPwdResult oJsonPwdResult,string authtoken)
        {
            try
            {
                dashboardrequest odashboardrequest = new dashboardrequest();    odashboardrequest.header = new headerrequest();
                odashboardrequest.header.formname = "FO-013-DSBRD";
                odashboardrequest.roleCd = "";
                odashboardrequest.serviceName = "dashboardMenuService";

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(odashboardrequest.GetType(), odashboardrequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/loginapi/auth/dashboard", "POST", oReturnStatus.pro_description, authtoken);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                oJsonResult = new List<jsondashboardresult>();
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (List<jsondashboardresult>)oReturnStatus.pro_object;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
