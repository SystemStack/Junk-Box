using System;
using System.Data;
using System.Configuration;
using System.Web;
using FindingAPI_WebApp_Sample.com.ebay.developer;
using System.Net;

namespace FindingAPI_WebApp_Sample
{
    public class CustomFindingService : FindingService
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            try
            {   
                HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(uri);
                request.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME", ConfigurationManager.AppSettings["appID"]);
                request.Headers.Add("X-EBAY-SOA-OPERATION-NAME", "findItemsByKeywords");
                request.Headers.Add("X-EBAY-SOA-SERVICE-NAME", "FindingService");
                request.Headers.Add("X-EBAY-SOA-MESSAGE-PROTOCOL", "SOAP11");
                request.Headers.Add("X-EBAY-SOA-SERVICE-VERSION", "1.0.0");
                request.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
