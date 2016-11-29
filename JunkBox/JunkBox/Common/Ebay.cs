﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web;

using JunkBox.Models;
using JunkBox.DataAccess;
using System.Linq;

namespace JunkBox.Common
{

    public class EbayAccessToken
    {
        private static string appIdSandbox = ConfigurationManager.AppSettings["AppIDSandBox"]; //Our 'Client ID'
        private static string certIdSandbox = ConfigurationManager.AppSettings["CertIDSandBox"];//Our 'Client Secret'

        public static IDictionary<string, object> RequestApplicationAccessToken()
        {
            /*
             * The Authorization header requires a Base64-encoded value that is comprised of the client ID and client secret values.
             * To generate this value, combine your application's client ID and client secret values by separating them with a colon, 
             * and Base64 encode those combined values. In other words, Base64 encode the following: <client_id>:<client_secret>.
             * POST https://api.sandbox.ebay.com/identity/v1/oauth2/token
             
                HTTP headers:
                    Content-Type = application/x-www-form-urlencoded
                    Authorization = Basic <B64-encoded-oauth-credentials>

                HTTP method: POST

                URL: https://api.sandbox.ebay.com/identity/v1/oauth2/token

                Request body (wrapped for readability):
                    grant_type=client_credentials&
                    redirect_uri=<redirect_URI>&
                    scope=https://api.ebay.com/oauth/api_scope


                EXAMPLE RESPONSE:
                    access_token : "[long ass sequence of characters]"
                    expires_in : 7200
                    refresh_token : "N/A"
                    token_type : "Application Access Token"
             */

            var plainTextBytes = Encoding.UTF8.GetBytes(appIdSandbox + ":" + certIdSandbox);
            string authHeader = Convert.ToBase64String(plainTextBytes);
            string urlApi = "https://api.sandbox.ebay.com/identity/v1/oauth2/token";

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["grant_type"] = "client_credentials";
            query["redirect_uri"] = "";
            query["scope"] = "https://api.ebay.com/oauth/api_scope https://api.ebay.com/oauth/api_scope/buy.guest.order";

            return Web.PostTokenRequest(urlApi, authHeader, query.ToString());
        }

        private static IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        public static bool IsAccessTokenValid()
        {
            Dictionary<string, string> accessToken = dataAccess.Select("SELECT * FROM AccessToken WHERE UseType='ApplicationAccessToken'").First();

            DateTime creationTime = DateTime.Parse(accessToken["DateCreated"]);
            DateTime currentTime = DateTime.Now;

            double timeDifference = (currentTime - creationTime).TotalSeconds;
            double expireLength = Double.Parse(accessToken["ExpiresIn"]);

            if (timeDifference <= expireLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetAccessToken()
        {
            Dictionary<string, string> accessToken = dataAccess.Select("SELECT AccessToken FROM AccessToken WHERE UseType='ApplicationAccessToken'").First();

            return accessToken["AccessToken"];
        }

        public static int UpdateAccessToken()
        {
            DateTime updateTime = DateTime.Now;
            IDictionary<string, object> tokenResponse = RequestApplicationAccessToken();

            Dictionary<string, string> updateParameters = new Dictionary<string, string>() {
                { "AccessToken",  tokenResponse["access_token"].ToString() },
                { "ExpiresIn",    tokenResponse["expires_in"].ToString() },
                { "RefreshToken", tokenResponse["refresh_token"].ToString() },
                { "DateCreated",  updateTime.ToString("yyyy-MM-dd HH:mm:ss") }
            };

            return dataAccess.Update("AccessToken", updateParameters, "UseType", "ApplicationAccessToken");
        }
    }

    public class EbayOrderAPI
    {
        private static string authToken = ConfigurationManager.AppSettings["AuthToken"];
        private static string baseUrl = "https://api.sandbox.ebay.com";


        public static IDictionary<string, object> PlaceGuestOrder(string checkoutSessionId)
        {
            //POST https://api.sandbox.ebay.com/buy/order/v1/guest_checkout_session/{guest_checkoutsession_id}/place_order

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/" + checkoutSessionId + "/place_order";

            return Web.PostWebRequest(apiUrl, "");
        }


        public static IDictionary<string, object> UpdateGuestSessionPaymentInfo(string checkoutSessionId, Dictionary<string, string> customerInfo, Dictionary<string, string> addressInfo)
        {

            /*
             * POST: https://api.sandbox.ebay.com/buy/order/v1/guest_checkout_session/{guest_checkoutsession_id}/update_payment_info
             * 
             *{ /* UpdatePaymentInformation 
                "creditCard":
                { /* CreditCard 
                    "accountHolderName": string,
                    "billingAddress":
                    { /* BillingAddress 
                        "addressLine1": string,
                        "addressLine2": string,
                        "city": string,
                        "country": string,
                        "county": string,
                        "firstName": string,
                        "lastName": string,
                        "postalCode": string,
                        "stateOrProvince": string
                    },
                    "brand": string,
                    "cardNumber": string,
                    "cvvNumber": string,
                    "expireMonth": integer,
                    "expireYear": integer
                }
              }
            */

            EbayUpdateGuestSessionPaymentInfoModel payload = new EbayUpdateGuestSessionPaymentInfoModel();

            EbayCreditCardModel creditCard = new EbayCreditCardModel();
            creditCard.accountHolderName = "Frank Smith";
            creditCard.brand = "MASTERCARD";
            creditCard.cardNumber = "5100000001598174";
            creditCard.cvvNumber = "012";
            creditCard.expireMonth = 10;
            creditCard.expireYear = 2019;

            EbayBillingAddressModel billingAddress = new EbayBillingAddressModel();
            billingAddress.firstName = "Frank";
            billingAddress.lastName = "Smith";
            billingAddress.addressLine1 = "3737 Any St";
            billingAddress.addressLine2 = "";
            billingAddress.city = "San Jose";
            billingAddress.stateOrProvince = "CA";
            billingAddress.postalCode = "95134";
            billingAddress.country = "US";

            creditCard.billingAddress = billingAddress;
            payload.creditCard = creditCard;

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/" + checkoutSessionId + "/update_payment_info";

            var json_serializer = new JavaScriptSerializer();
            string postBody = json_serializer.Serialize(payload);
            //System.Windows.Forms.MessageBox.Show(postBody);

            return Web.PostWebRequest(apiUrl, postBody);
        }


        public static IDictionary<string, object> InitiateGuestCheckoutSession(string itemId, Dictionary<string, string> customerInfo, Dictionary<string, string> addressInfo)
        {

            /*
             * Initiate a guest checkout session
                POST https://api.sandbox.ebay.com/buy/order/v1/guest_checkout_session/initiate

                {
                    "contactEmail" : "fsmith1234@gmail.com",
                    "contactFirstName":"Frank",
                    "contactLastName":"Smith",
                    "shippingAddress" : {
                        "recipient" : "Frank Smith",
                        "phoneNumber" : "617 817 7449 ",
                        "addressLine1" : "3737 Casa Verde St",
                        "city" : "San Jose",
                        "stateOrProvince" : "CA",
                        "postalCode" : "95134",
                        "country" : "US"
                    },
                    "lineItemInputs" : [ {
                        "quantity" : 1,
                        "itemId" : "v1|190006102824|0"
                    }
                    ]
                }
            */
            EbayGuestCheckoutSessionRequestModel payload = new EbayGuestCheckoutSessionRequestModel();
            payload.contactEmail = customerInfo["Email"];
            payload.contactFirstName = customerInfo["FirstName"];
            payload.contactLastName = customerInfo["LastName"];

            EbayShippingAddressModel shipping = new EbayShippingAddressModel();
            shipping.recipient = customerInfo["FirstName"] + " " + customerInfo["LastName"];
            shipping.phoneNumber = customerInfo["Phone"];
            shipping.addressLine1 = addressInfo["ShippingAddress"];
            shipping.addressLine2 = addressInfo["ShippingAddress2"];
            shipping.city = addressInfo["ShippingCity"];
            shipping.stateOrProvince = addressInfo["ShippingState"];
            shipping.postalCode = addressInfo["ShippingZip"];
            shipping.country = "US";

            payload.shippingAddress = shipping;

            EbayLineItemModel[] lineItems = new EbayLineItemModel[] {
                new EbayLineItemModel { quantity = 1, itemId = itemId }
            };

            payload.lineItemInputs = lineItems;

            string apiUrl = baseUrl + "/buy/order/v1/guest_checkout_session/initiate";

            var json_serializer = new JavaScriptSerializer();
            string postBody = json_serializer.Serialize(payload);

            return Web.PostWebRequest(apiUrl, postBody);
        }
    }

    public class EbayBrowseAPI
    {
        private static string authToken = ConfigurationManager.AppSettings["AuthToken"];
        private static string baseUrl = "https://api.sandbox.ebay.com";
        /*
         *  ITEM                   
         *      GET /item/{item_id}	            Retrieves the details of the specified item.	view
         *
         *  ITEM_FEED         
         *      GET /item_feed	                Returns the items in the specified feed file.	view
         *
         *  ITEM_GROUP       
         *      GET /item_group/{item_group_id}	Retrieves the specified group of items.	view
         *
         *  ITEM_SUMMARY 
         *      GET /item_summary/search        Searches for eBay items by keyword. Optionally, you can apply filters, sort the list, and control the number of items returned on each page of data.
         */


        public static IDictionary<string, object> ItemSummarySearch(string categoryId, string maxPrice)
        {
            /*
             * GET https://api.ebay.com/buy/browse/v1/item_summary/search?
             *      category_ids=string&
             *      filter=FilterField&     &filter=price[..200] which means price.value is <= 200
             *      limit=string&           //priceCurrency	filter=priceCurrency:USD //priceCurrency must be used if price is used
             *      offset=string&
             *      q=string&
             *      sort=SortField
             */

            string apiUrl = baseUrl + "/buy/browse/v1/item_summary/search";
            Dictionary<string, List<object>> urlParameters = new Dictionary<string, List<object>>() {
                { "category_ids",   new List<object>() { categoryId } },
                { "filter",         new List<object>() { "price:[.." + maxPrice + "]",
                                                         "priceCurrency:USD" } },
                { "limit",          new List<object>() { "10" } },
                { "offset",         new List<object>() { "10" } },
                
                //{"q",             new List<object>() { "" } }
                //{"sort",          new List<object>() { "" } }
            };

            return Web.GetWebRequest(apiUrl, Web.BuildQueryString(urlParameters));
        }
    }


    //Still needed for getting all categories
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
                //System.Windows.Forms.MessageBox.Show(dataObjects.ToString());
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