using NewPANValidate.FVUUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static NewPANValidate.applicationmain;

namespace NewPANValidate.fileincometaxupload
{
    public class fileincometaxuploadvalidate
    {
        public jsonfileincometaxuploadresult oJsonResult;
        WebClient oWebClient;
        public fileincometaxuploadvalidate(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
        public bool Validate(JsonUserResult oJsonUserResult,string authtoken)
        {
            try
            {
                fileincometaxuploadrequest ofileincometaxuploadrequest = new fileincometaxuploadrequest();
                ofileincometaxuploadrequest.entityNum = oJsonUserResult.entity;
                ofileincometaxuploadrequest.roleCd = oJsonUserResult.role;
                ofileincometaxuploadrequest.serviceName = "fileIncomeTaxUploadService";

                NewJson oJson = new NewJson(oWebClient);
                oReturnStatus = oJson.JsonStringFormat(ofileincometaxuploadrequest.GetType(), ofileincometaxuploadrequest);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                oReturnStatus = oJson.API_Request("https://eportal.incometax.gov.in/iec/servicesapi/auth/saveEntity", "POST", oReturnStatus.pro_description, authtoken);
                if (oReturnStatus.pro_status == false) { throw new Exception("An Error occurred While Generating Token " + oReturnStatus.pro_description); }

                oJsonResult = new jsonfileincometaxuploadresult();
                oReturnStatus = oJson.Json_Return(oReturnStatus.pro_description, oJsonResult);
                if (oReturnStatus.pro_status == false) { throw new Exception(oReturnStatus.pro_description); }

                oJsonResult = (jsonfileincometaxuploadresult)oReturnStatus.pro_object;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
