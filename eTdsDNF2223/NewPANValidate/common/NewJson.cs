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
    public class NewJson
    {
        private MemoryStream oMemoryStream;
        private MemoryStream ResultMemoryStream;
        private DataContractJsonSerializer oDataContractJsonSerializer;
        WebClient oWebClient;
        public NewJson(WebClient _WebClient)
        {
            oWebClient = _WebClient;
        }
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

        public ReturnStatus API_Request(String Str_Url,String Method,String Str_data,string authtoken="")
        {
            try
            {
                string result = string.Empty;string strtoken = string.Empty;
                oWebClient.Encoding = Encoding.UTF8;
                oWebClient.Headers.Add("Content-Type", "application/json");
                if (authtoken != string.Empty) oWebClient.Headers["Cookie"] = authtoken;
                //if (authtoken != string.Empty) oWebClient.Headers.Set(HttpRequestHeader.Cookie, authtoken);
                //if (authtoken != string.Empty) oWebClient.Headers.Add(HttpRequestHeader.Cookie, authtoken);
                result = oWebClient.UploadString(Str_Url, Method, Str_data);
                strtoken = oWebClient.ResponseHeaders["Set-Cookie"];
                //CookieContainer c = new CookieContainer();
                //var cookies = c.GetCookies(new Uri("incometax.gov.in")); foreach (Cookie co in cookies) { co.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1)); }
                return new ReturnStatus(true, result, strtoken);
            }
            catch (Exception ex)
            {
                return new ReturnStatus(false, ex.Message);
            }
        }
    }
}
