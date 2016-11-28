using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using System.Linq;

using JunkBox.DataAccess;
using JunkBox.Models;
using JunkBox.Common;
using System.Web.Script.Serialization;

namespace JunkBox.Controllers
{
    public class EbayController : Controller
    {
        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        private static string appId = ConfigurationManager.AppSettings["AppID"];
        private static string appIdSandbox = ConfigurationManager.AppSettings["AppIDSandBox"];


        //POST: Ebay/GetAllCategories/
        [HttpPost]
        public ActionResult GetAllCategories()
        {
            /*
             http://open.api.ebay.com/Shopping?
                    callname=GetCategoryInfo&
                    appid=YourAppID&
                    version=967&
                    siteid=0&
                    CategoryID=-1&
                    IncludeSelector=ChildCategories
             */
            string URL = "http://open.api.ebay.com/Shopping";

            Dictionary<string, string> urlParameters = new Dictionary<string, string>() {
                { "callname", "GetCategoryInfo"},
                { "appid", appId},
                { "responseencoding", "JSON"},
                { "version", "967"},
                { "siteid", "0"},
                { "CategoryID", "-1"},
                { "IncludeSelector", "ChildCategories"}
            };

            return Json(Ebay.GetEbayResult(URL, urlParameters));
        }

        //POST: Ebay/BrowseApiFindViableItems/{data}
        [HttpPost]
        public ActionResult BrowseApiFindViableItems(EbayBrowseAPIModel data)
        {
            //Get customer info
            Dictionary<string, string> customerInfo = dataAccess.Select("SELECT CustomerID, QueryID FROM Customer WHERE Email='" + data.email + "'").First();

            //Get customer query prefrences 
            Dictionary<string, string> queryPref = dataAccess.Select("SELECT * FROM Query WHERE QueryID='" + customerInfo["QueryID"] + "'").First();


            return Json(EbayBrowseAPI.ItemSummarySearch(queryPref["CategoryID"], queryPref["PriceLimit"]));
        }

        //POST: Ebay/OrderApiInitiateGuestCheckoutSession/{data}
        public ActionResult OrderApiInitiateGuestCheckoutSession(EbayOrderApiInitiateGuestCheckoutSessionModel data)
        {
            //Get customer info
            Dictionary<string, string> customerInfo = dataAccess.Select("SELECT * FROM Customer WHERE Email='" + data.email + "'").First();

            //Get customer address 
            Dictionary<string, string> addressInfo = dataAccess.Select("SELECT * FROM Address WHERE AddressID='" + customerInfo["AddressID"] + "'").First();

            IDictionary<string, object> response = EbayOrderAPI.InitiateGuestCheckoutSession(data.orderId, customerInfo, addressInfo);

            string checkoutSessionId = response["checkoutSessionId"].ToString();
            string expirationDate = response["expirationDate"].ToString();

            IDictionary<string, object> pricingSummary = (IDictionary<string,object>)response["pricingSummary"];
            IDictionary<string, object> total = (IDictionary<string, object>)pricingSummary["total"];

            string totalPrice = total["value"].ToString();

            //Need to insert... CustomerID, AddressID, PurchasePrice, CheckoutSessionID, ExpirationDate, ImageURL
            Dictionary<string, string> parameters = new Dictionary<string, string>() {
                { "CustomerID", customerInfo["CustomerID"]},
                { "AddressID", customerInfo["AddressID"]},
                { "PurchasePrice", totalPrice},
                { "CheckoutSessionID", checkoutSessionId},
                { "ExpirationDate", expirationDate},
                { "ImageURL", data.imageUrl}
            };
            int insertResult = dataAccess.Insert("CustomerOrder", parameters);

            JsonResult result = Json(response);

            return result;
        }

        //POST: Ebay/OrderApiPlaceGuestOrder/{data}
        public ActionResult OrderApiPlaceGuestOrder(EbayOrderApiPlaceGuestOrderModel data)
        {
            /*
            //Get customer info
            Dictionary<string, string> customerInfo = dataAccess.Select("SELECT * FROM Customer WHERE Email='" + data.email + "'").First();

            //Get customer address 
            Dictionary<string, string> addressInfo = dataAccess.Select("SELECT * FROM Address WHERE AddressID='" + customerInfo["AddressID"] + "'").First();

            IDictionary<string, object> initiateResponse = EbayOrderAPI.InitiateGuestCheckoutSession(data.orderId, customerInfo, addressInfo);
            string json = new JavaScriptSerializer().Serialize(Json(initiateResponse).Data);
            System.Windows.Forms.MessageBox.Show(json);

            string checkoutSessionId = initiateResponse["checkoutSessionId"].ToString();
            string expirationDate = initiateResponse["expirationDate"].ToString();

            IDictionary<string, object> pricingSummary = (IDictionary<string, object>)initiateResponse["pricingSummary"];
            IDictionary<string, object> total = (IDictionary<string, object>)pricingSummary["total"];



            IDictionary<string, object> updatePaymentResponse = EbayOrderAPI.UpdateGuestSessionPaymentInfo(checkoutSessionId, customerInfo, addressInfo);
            json = new JavaScriptSerializer().Serialize(Json(updatePaymentResponse).Data);
            System.Windows.Forms.MessageBox.Show(json);



            IDictionary<string, object> placeOrderResponse = EbayOrderAPI.PlaceGuestOrder(checkoutSessionId);
            json = new JavaScriptSerializer().Serialize(Json(placeOrderResponse).Data);
            System.Windows.Forms.MessageBox.Show(json);


            string totalPrice = total["value"].ToString();

            //Need to insert... CustomerID, AddressID, PurchasePrice, CheckoutSessionID, ExpirationDate, ImageURL
            Dictionary<string, string> parameters = new Dictionary<string, string>() {
                { "CustomerID", customerInfo["CustomerID"]},
                { "AddressID", customerInfo["AddressID"]},
                { "PurchasePrice", totalPrice},
                { "CheckoutSessionID", checkoutSessionId},
                { "ExpirationDate", expirationDate},
                { "ImageURL", data.imageUrl}
            };
            int insertResult = dataAccess.Insert("CustomerOrder", parameters);

            //JsonResult result = Json(response);
            JsonResult result = Json(placeOrderResponse);
            return result;
            */

            //Testing out Authorization
            return Json(EbayAccessToken.RequestApplicationAccessToken());
        }
    }
}
 