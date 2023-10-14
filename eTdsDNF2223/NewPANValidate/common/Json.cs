using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using ServiceStack.Host;
using System.Net.Http;

namespace NewPANValidate
{
    public class Json
    {
        private MemoryStream oMemoryStream;
        private MemoryStream ResultMemoryStream;
        private DataContractJsonSerializer oDataContractJsonSerializer;
        public ReturnStatus JsonStringFormat(System.Type obj_type,object oApi_List)
        {
            try
            {
                oMemoryStream = new MemoryStream();
                oDataContractJsonSerializer = new DataContractJsonSerializer(obj_type);
                oDataContractJsonSerializer.WriteObject(oMemoryStream, oApi_List);
                oMemoryStream.Position = 0;

                StreamReader oStreamReader = new StreamReader(oMemoryStream);
                string StrJson = oStreamReader.ReadToEnd();

                return new ReturnStatus(true, StrJson);
            }
            catch (Exception ex)
            {
                return new ReturnStatus(false, ex.Message);
            }
        }
        public byte[] UnicodeStringToBytes(string Str)
        {
            return System.Text.Encoding.UTF8.GetBytes(Str);
        }
        public ReturnStatus Json_Return(String result,Object oApi_List)
        {
            try
            {
                ResultMemoryStream = new MemoryStream(UnicodeStringToBytes(result));
                ResultMemoryStream.Position = 0;

                oDataContractJsonSerializer = new DataContractJsonSerializer(oApi_List.GetType());
                oApi_List = oDataContractJsonSerializer.ReadObject(ResultMemoryStream);

                return new ReturnStatus(true, oApi_List);
            }
            catch(Exception ex)
            {
                return new ReturnStatus(false, ex.Message);
            }
        }

        public ReturnStatus API_Request(String Str_Url,String Method,String Str_data)
        {
            try
            {
                string result = string.Empty;
                using (WebClient oWebClient = new WebClient())
                {
                    oWebClient.Encoding = Encoding.UTF8;
                    oWebClient.Headers["Content-Type"] = "application/json";
                    oWebClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    oWebClient.Headers.Add("Cache-Control", "no-cache");
                    result = oWebClient.UploadString(Str_Url, Method, Str_data);
                    oWebClient.Dispose();
                }
                return new ReturnStatus(true, result);
            }
            catch (Exception ex)
            {
                return new ReturnStatus(false, ex.Message);
            }
        }
    }
}
