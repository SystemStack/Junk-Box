using JunkBox.com.ebay.developer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web;

namespace JunkBox.Common
{

    public class EbayOrderAPI
    {
        /*
         * Initiate a guest checkout session
            POST https://api.ebay.com/buy/order/v1/guest/checkout_session/initiate
			
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

            
        It generates a response:
        {
  "checkoutSessionId": "5016712327",
  "expirationDate": {
    "value": "2036-08-11T23:03:18.501Z"
  },
  "lineItems": [
    {
      "itemId": "v1|190006102824|0",
      "title": "display phones",
      "shortDescription": "New",
      "imageUrl": "http://i.ebayimg.qa.ebay.com/00/s/NzY4WDEwMjQ=/z/0ZkAAOSw3dFWHCF8/$_1.JPG?set_id=880000500F",
      "seller": {
        "username": "oneStopShop"
      },
      "quantity": 1,
      "lineItemId": "5195728827",
      "baseUnitPrice": {
        "value": 20,
        "currency": "USD"
      },
      "shippingOptions": [
        {
          "selected": true,
          "shippingOptionId": "5274330495",
          "shippingServiceName": "USPS Parcel Select Ground",
          "shippingCarrierName": "USPS",
          "minEstimatedDeliveryDate": {
            "value": "2016-08-15T07:00:00.000Z"
          },
          "maxEstimatedDeliveryDate": {
            "value": "2016-08-23T07:00:00.000Z"
          },
          "baseDeliveryCost": {
            "value": 0,
            "currency": "USD"
          }
        },
        {
          "selected": false,
          "shippingOptionId": "5274330507",
          "shippingServiceName": "USPS Retail Ground",
          "shippingCarrierName": "USPS",
          "minEstimatedDeliveryDate": {
            "value": "2016-08-15T07:00:00.000Z"
          },
          "maxEstimatedDeliveryDate": {
            "value": "2016-08-23T07:00:00.000Z"
          },
          "baseDeliveryCost": {
            "value": 1,
            "currency": "USD"
          }
        }
      ],
      "netPrice": {
        "value": 20,
        "currency": "USD"
      }
    }
  ],
  "shippingAddress": {
    "addressLine1": "3737 Casa Verde St",
    "city": "San Jose",
    "stateOrProvince": "CA",
    "postalCode": "95134",
    "country": "US",
    "recipient": "Frank Smith",
    "phoneNumber": "617 817 7449 "
  },
  "pricingSummary": {
    "priceSubtotal": {
      "value": 20,
      "currency": "USD"
    },
    "baseDeliveryCost": {
      "value": 0,
      "currency": "USD"
    },
    "tax": {
      "value": 0,
      "currency": "USD"
    },
    "adjustment": {
      "value": 0,
      "currency": "USD"
    },
    "total": {
      "value": 20,
      "currency": "USD"
    }
  },
  "acceptedPaymentMethods": [
    {
      "paymentMethodMessages": [
        {
          "legalMessage": "PayPal processes payments for eBay. A PayPal account isn't required."
        }
      ]
    }
  ]
}

        Place the order:
        POST https://api.ebay.com/buy/order/v1/guest_checkout_session/{checkoutSessionId}/place_order

        it gives a response:
        {
  "purchaseOrderId": "5833850019",
  "purchaseOrderHref": "buy/order/v1/purchase_order/5833850019"
}

        get info about the purchase order (ebay member only? no guest?)
        GET https://api.ebay.com/buy/order/v1/purchase_order/98262585

         
         */
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
                { "category_ids", new List<object>() { categoryId } },
                { "filter",     new List<object>() { "price:[.." + maxPrice + "]",
                                                     "priceCurrency:USD" } },
                { "limit",      new List<object>() { "10" } },
                { "offset",     new List<object>() { "10" } },
                
                //{"q",           new List<object>() { "" } }
                //{"sort", ""}
            };

            return GetWebRequest(apiUrl, BuildQueryString(urlParameters));
        }

        private static string BuildQueryString(IDictionary<string, List<object>> parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach(KeyValuePair<string, List<object>> entry in parameters)
            {
               foreach(object value in entry.Value)
               {
                    query.Add(entry.Key, (string)value);
               }
            }
            return "?" + query.ToString();
        }

        private static IDictionary<string, object> GetWebRequest(string URL, string query)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

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
    }


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