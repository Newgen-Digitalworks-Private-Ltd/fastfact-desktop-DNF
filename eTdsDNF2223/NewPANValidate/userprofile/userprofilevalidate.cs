using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate.userprofile
{
    public class userprofilevalidate
    {
        public jsonuserprofileresult oJsonResult;
        WebClient oWebClient;
        public userprofilevalidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(string authtoken)
        {
            try
            {
                userprofilerequest ouserprofilerequest = new userprofilerequest();
                ouserprofilerequest.serviceName = "userProfileService";

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(ouserprofilerequest.GetType(), ouserprofilerequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/servicesapi/auth/saveEntity", "POST", oReturnStatus.pro_description, authtoken);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                oJsonResult = new jsonuserprofileresult();
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (jsonuserprofileresult)oReturnStatus.pro_object;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
