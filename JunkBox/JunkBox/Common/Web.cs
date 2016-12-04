using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

using JunkBox.Ebay;
using System.IO;
using System.Runtime.Serialization.Json;

namespace JunkBox.Common
{
    public class Web
    {

        public static T Get<T>(string uri)
        {
            string accessToken = GetAccessToken();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = client.GetStreamAsync(uri))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                    return (T)serializer.ReadObject(response.Result);
                }
            }
        }

        public static T Post<T>(string uri)
        {
            return Post<T, object>(uri, null);
        }

        public static T Post<T, K>(string uri, K postBody)
        {
            string accessToken = GetAccessToken();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = client.PostAsJsonAsync(uri, postBody).Result)
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                   
                    return (T)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
                }
            }
        }

        public static IDictionary<string, object> PostTokenRequest(string URL, string authorization, string postBody)
        {
            HttpClient client = new HttpClient();

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

        private static string GetAccessToken()
        {
            if (!Auth.IsAccessTokenValid())
            {
                Auth.UpdateAccessToken();
            }
            return Auth.GetAccessToken();
        }
    }
}