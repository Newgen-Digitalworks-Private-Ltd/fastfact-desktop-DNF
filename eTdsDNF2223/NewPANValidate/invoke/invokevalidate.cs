using NewPANValidate.FVUUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate.invoke
{
    public class invokevalidate
    {
        public jsoninvokeresult oJsonResult;
        WebClient oWebClient;
        public invokevalidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(JsonUserResult oJsonUserResult, string authtoken)
        {
            try
            {
                invokerequest oinvokerequest = new invokerequest();
                oinvokerequest.metadata = new metadatarequest();
                oinvokerequest.metadata.entityType = "P";
                oinvokerequest.metadata.filingType = "O";
                oinvokerequest.metadata.refYearType = "NA";
                oinvokerequest.metadata.entityType = "P";
                oinvokerequest.metadata.entityType = "P";
                oinvokerequest.metadata.entityType = "P";
                oinvokerequest.metadata.financialQtr = "0";
                oinvokerequest.metadata.refYear = "2021";
                oinvokerequest.metadata.operation = "get";
                oinvokerequest.metadata.formName = "FTDS";
                oinvokerequest.metadata.sn = "manageForm";

                oinvokerequest.data = new datarequest();
                oinvokerequest.data.entityNumber = oJsonUserResult.entity;

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(oinvokerequest.GetType(), oinvokerequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/itfweb/auth/invoke", "POST", oReturnStatus.pro_description, authtoken);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                oJsonResult = new jsoninvokeresult();
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (jsoninvokeresult)oReturnStatus.pro_object;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
