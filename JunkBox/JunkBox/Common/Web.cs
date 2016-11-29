using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace JunkBox.Common
{
    public class Web
    {

        public static string BuildQueryString(IDictionary<string, List<object>> parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (KeyValuePair<string, List<object>> entry in parameters)
            {
                foreach (object value in entry.Value)
                {
                    query.Add(entry.Key, (string)value);
                }
            }
            return "?" + query.ToString();
        }

        public static IDictionary<string, object> GetWebRequest(string URL, string query)
        {
            if(!EbayAccessToken.IsAccessTokenValid())
            {
                EbayAccessToken.UpdateAccessToken();
            }
            string accessToken = EbayAccessToken.GetAccessToken();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // List data response.
            HttpResponseMessage response = client.GetAsync(query).Result;  // Blocking call!
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
                    { response.StatusCode.ToString(), "(" + response.ReasonPhrase + ")" }
                };
            }
        }

        public static IDictionary<string, object> PostWebRequest(string URL, string postBody)
        {
            if (!EbayAccessToken.IsAccessTokenValid())
            {
                EbayAccessToken.UpdateAccessToken();
            }
            string accessToken = EbayAccessToken.GetAccessToken();

            HttpClient client = new HttpClient();

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // List data response.
            HttpResponseMessage response = client.PostAsync(URL, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
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
                    { response.StatusCode.ToString(), "(" + response.ReasonPhrase + ")" }
                };
            }
        }

        public static IDictionary<string, object> PostTokenRequest(string URL, string authorization, string postBody)
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);

            // List data response.
            HttpResponseMessage response = client.PostAsync(URL, new StringContent(postBody, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
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
                    { response.StatusCode.ToString(), "(" + response.ReasonPhrase + ")" }
                };
            }
        }
    }
}