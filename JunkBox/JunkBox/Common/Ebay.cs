using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web;

using JunkBox.Models;

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
             * To generate this value, combine your application's client ID and client secret values by separating them with a colon, and Base64 encode those combined values. In other words, Base64 encode the following: <client_id>:<client_secret>.
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
            access_token : "v^1.1#i^1#p^1#I^3#r^0#f^0#t^H4sIAAAAAAAAAOVXa2wUVRTutt01DVQEBAzVspkqRMjM3pndzu5O2DXbB2WhtMVdeVQFZ2futNPOzkzuvWu7hthSFY0hQUMiqbWxYIyiARUfCAk/SEzESOIrJIqRHxJBMGICNYox4p3tUrbVAMJqSJw/k3vOued+5zvn3Afo91Qs3LR00y+VrptKR/pBf6nLxU8BFR73opvLSue6S0CBgWuk/87+8oGy7xdjOW3Y0r0Q25aJobc3bZhYygkjTAaZkiVjHUumnIZYIoqUiK1olgQOSDayiKVYBuONN0QYEYZlOSQIKX9QDSpKkErNiz6TVoRRw0EQBhoMK1owHAJ+qsc4A+MmJrJJIowAeJHleVYIJfmwJPglP+B4MdzOeFdBhHXLpCYcYKI5uFJuLirAenmoMsYQEeqEicZjSxKtsXhDY0tysa/AVzTPQ4LIJIMnjuotFXpXyUYGXn4ZnLOWEhlFgRgzvujYChOdSrGLYK4B/hjVfpASlECtWKvwYkBQikLlEgulZXJ5HI5EV1ktZypBk+gkeyVGKRupLqiQ/KiFuog3eJ3fyoxs6JoOUYRprIutjbW1MdHVskEgWm2xyzJmd53Vyybq1rBioBaqIuD9rCIEeH9IlPPrjDnLszxpoXrLVHWHM+xtsUgdpKDhZGr4AmqoUavZimIacQAV2An8RQoDYruT07EkZkin6aQVpikP3tzwygkYn00I0lMZAsc9TFbkGIowsm3rKjNZmSvFfPX04gjTSYgt+Xw9PT1cj5+zUIdPAID3rVnRnFA6YVpmHFun13P2+pUnsHouFAXSmViXSNamWHppqVIAZgcTFUQhGBLyvE+EFZ0s/YugIGbfxIYoVoOEIa9qoUBtOBAIyqlAsBgNEs3XqM/BAVNylk3LqBsS25AVyCq0zjJpiHRV8tdqgj+kQVYVwxobCGsam6pV6XoahADCVEoJh/5HfXK1lV5v6FSZpJVWnHIvVqkvtTCB6tWW+t+GllAsG7ZZhq5k/6PYnF6/yvj8SG2TEcnWZbJ0nICGQX/XFa6Sy+T6Yu1bxUrkP2uZawtdl8mNFTQfoFctkQc8uL646G3mhopLsdKcswlzSLaJhTgKzTYg5hDEVgbR2xfX6hzJSasbmnSHI8gyDIhW8dfFAnb6+Mbigc4v3/i4jakP2dbHKKHc+CyZRu+I1udA/wsntW/isyFakvv4AdcBMODaR18eIAhYfhG421N2X3nZVAbrBNI0mWrK6uV0WeOw3mHSWzGCXDfM2rKOSj2uFcc3r+0reLCMPAhuG3+yVJTxUwreL+D2Sxo3P21OJS/yvBDiw4LfD9pBzSVtOT+7/NZdo/V79ls1O3ZO6frh/WHu+dnDTdNB5biRy+UuKR9wlQy6PHvf+rxq1sGZnx1bNn/Bm6/Wtw2t3Hv2/O+etzvOH5m/YR2cOxRvUA6ONp0FelV1z4mhd4aHKiqZ6sOHZj928t2W5nldu/rKtvQ1pn+eNfrclxtH5j1bfXpHyH3EbZxcu/OnFy5Mnbm3ZsvGmajZPQ0ZNSsPLDg2zRx6/S6y9bvknD0zvjk+dbR6e+u5quGFpwbPDO5u3BQ5MXfryw+tO93Vvj8ZeLHKPNOHvA98EhHqPtipvPfUmqqmp2c88aOob3/jteWvLHvy0OqKxiUzTtzy6Vd/PKwkjn68PH7m1G8e1HmHcGHD4NcfPdPz69CH0498+0XHo48kRvdvG7mn5tj985XD2kvatqO7z587u28sjX8CD66ymUoOAAA="
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
            query["scope"] = "https://api.ebay.com/oauth/api_scope";


            return Web.PostTokenRequest(urlApi, authHeader, query.ToString());
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