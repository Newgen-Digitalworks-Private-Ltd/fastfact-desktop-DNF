using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate.PANAadharStatus
{
    public class PANAadharValidate
    {
        public JsonResult oJsonResult = null;
        public bool Validate(string aadhar,string PAN)
        {
            try
            {
                PANAadharRequest oPANAadharRequest = new PANAadharRequest();
                oPANAadharRequest.aadhaarNumber = aadhar;
                oPANAadharRequest.pan = PAN;
                oPANAadharRequest.preLoginFlag = "Y";
                oPANAadharRequest.serviceName = "linkAadhaarPreLoginService";

                Json oJson = new Json();
                oReturnStatus = oJson.JsonStringFormat(oPANAadharRequest.GetType(), oPANAadharRequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string URL = "https://eportal.incometax.gov.in/iec/servicesapi/getEntity";
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
