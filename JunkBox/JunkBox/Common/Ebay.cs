using JunkBox.com.ebay.developer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;

namespace JunkBox.Common
{
    public class Ebay
    {
        public static IDictionary<string, object> GetEbayResult(string URL, Dictionary<string, string> urlParameters)
        {
            StringBuilder urlParams = new StringBuilder();

            int count = 0;
            foreach(KeyValuePair<string, string> entry in urlParameters)
            {
                if (count == 0)
                {
                    if (entry.Value != "")
                        urlParams.Append("?" + entry.Key + "=" + entry.Value);
                    else
                        urlParams.Append("?" + entry.Key);
                }
                else
                {
                    if (entry.Value != "")
                        urlParams.Append("&" + entry.Key + "=" + entry.Value);
                    else
                        urlParams.Append("&" + entry.Key);
                }
                count++;
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParams.ToString()).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                var json_serializer = new JavaScriptSerializer();
                var routes_list = (IDictionary<string, object>)json_serializer.DeserializeObject(dataObjects);
                return routes_list;
            }
            else
            {
                return new Dictionary<string, object>() {
                    { response.StatusCode.ToString() , "(" + response.ReasonPhrase + ")" }
                };
            }
        }

        public static string GetTimestamp()
        {
            //App ID (Client ID)	WalterWo-JunkBox-SBX-645ed6013-c241386a
            //Dev ID	ad5f5bd0-374e-442a-a57f-ea202679bf77
            //Cert ID (Client Secret)	SBX-45ed60138e48-1d64-4760-a734-2aaa

            string endpoint = "https://api.sandbox.ebay.com/wsapi";
            string callName = "GeteBayOfficialTime";
            string siteId = "0";
            string appId = "WalterWo-JunkBox-SBX-645ed6013-c241386a";     // use your app ID
            string devId = "ad5f5bd0-374e-442a-a57f-ea202679bf77";     // use your dev ID
            string certId = "SBX-45ed60138e48-1d64-4760-a734-2aaa";   // use your cert ID
            string version = "405";
            // Build the request URL
            string requestURL = endpoint
            + "?callname=" + callName
            + "&siteid=" + siteId
            + "&appid=" + appId
            + "&version=" + version
            + "&routing=default";

            // Create the service
            eBayAPIInterfaceService service = new eBayAPIInterfaceService();

            // Assign the request URL to the service locator.
            service.Url = requestURL;

            // Set credentials
            service.RequesterCredentials = new CustomSecurityHeaderType();
            service.RequesterCredentials.eBayAuthToken = "AgAAAA**AQAAAA**aAAAAA**hls1WA**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wFk4GjAZeCpwqdj6x9nY+seQ**fgIEAA**AAMAAA**VbNr6OoDllWn4igFmFbdHPKS6h4eDTATmRVPfstl5y0dOpuNENJby6VH3eZ6LkMfxiWGOjmMoYGTc4+mxwkrrme6xS3zhWUS/fJcwYPJ/S9FDg8SVTZbmq0ex3IKDGDPPrsYcBG+BpP5YaXpt7R1wq2G2YEa6zZJqrd4uxL7FGfaqdeaS9agYZapfHjV365PPesYdVgED6E1s/SaCoSxIi9eO4ZbMAtFvRH6m9bhrnOH1yV1RNKMI9ST5At7iikN0vdzBk+OOhV4w8HBolOs8LoZvTDcij73Dhf9DYYc3WPso+h1hf/jdcFHOCcg1s6fnw3rVwpNBErHLVDexlOryUAgA+XDU3+++GZhHvIUuYjdrnLOEqjhPTk262AsCCyEDjnf0gfXAv4qsJKFcJxb3dsdQ8sk1T+PLs7rV0HA+mAcjvyv9BbrQk/uOQGUAavGqObJkjE5Hw3Q3rkbdHUsSMtvgrBj+NOs+Ntb493Rt8/Ei2bqmZv/eQoAGaNTlOf3sMoIMhyFMA0skqfyQHrunaCgmLeHQM1KLtaMtrftzxMXKNXkE0XCHjnXbulz23CZj391BFcyEVjQpkeK0XDrBIPOaPGT8JMTGAyvuOPf6GSq4NlmmTmhgewYJvxoSNLL3euV7yssCYK/yTyP4Ia64C34YQd6tvBQG9ECx3cp6WNEQDDpcRA87Yf/O80qtuaGiCxILv1TY8IV8qv5Nad9Mqyw0twuDFfICT2FH/G8w83dMMeuVKwrVWrDfTx184sF";    // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = appId;
            service.RequesterCredentials.Credentials.DevId = devId;
            service.RequesterCredentials.Credentials.AuthCert = certId;

            // Make the call to GeteBayOfficialTime
            GeteBayOfficialTimeRequestType request = new GeteBayOfficialTimeRequestType();
            request.Version = "405";
            GeteBayOfficialTimeResponseType response = service.GeteBayOfficialTime(request);
            //Console.WriteLine("The time at eBay headquarters in San Jose, California, USA, is:");
            //Console.WriteLine(response.Timestamp);

            return response.Timestamp.ToString();
        }
    }
}