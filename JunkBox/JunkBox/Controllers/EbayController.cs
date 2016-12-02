using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;

using JunkBox.DataAccess;
using JunkBox.Models;
using JunkBox.Ebay;

namespace JunkBox.Controllers
{
    public class EbayController : Controller
    {
        private static QueryTable queryTable = QueryTable.Instance();
        private static CustomerTable customerTable = CustomerTable.Instance();
        private static CustomerOrderTable customerOrderTable = CustomerOrderTable.Instance();
        private static AddressTable addressTable = AddressTable.Instance();

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

            return Json(Categories.GetEbayResult(URL, urlParameters));
        }

        //POST: Ebay/BrowseApiFindViableItems/{data}
        [HttpPost]
        public ActionResult BrowseApiFindViableItems(EbayBrowseAPIModel data)
        {
            SelectCustomerModel customerData = new SelectCustomerModel() {
                Email = data.email
            };
            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);

            if(customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            QueryResultModel queryPref = queryTable.SelectRecord(new SelectQueryModel() { CustomerUUID = customerResult.CustomerUUID });

            return Json(BrowseAPI.ItemSummarySearch(queryPref.CategoryID, queryPref.PriceLimit));
        }

        //POST: Ebay/OrderApiInitiateGuestCheckoutSession/{data}
        public ActionResult OrderApiInitiateGuestCheckoutSession(EbayOrderApiInitiateGuestCheckoutSessionModel data)
        {
            SelectCustomerModel customerData = new SelectCustomerModel() {
                    Email = data.email
            };

            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);

            if(customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid User" });
            }

            AddressResultModel addressData = addressTable.SelectRecord(new SelectAddressModel() { CustomerUUID = customerResult.CustomerUUID });

            //NOTE: Eventually convert this to a 'proper' model
            IDictionary<string, object> response = OrderAPI.InitiateGuestCheckoutSession(data.orderId, customerResult, addressData);


            IDictionary<string, object> pricingSummary = (IDictionary<string, object>)response["pricingSummary"];
            IDictionary<string, object> total = (IDictionary<string, object>)pricingSummary["total"];

            string totalPrice = total["value"].ToString();

            InsertCustomerOrderModel customerOrder = new InsertCustomerOrderModel() {
                CustomerUUID = customerResult.CustomerUUID,
                CheckoutSessionID = (string)response["checkoutSessionId"],
                ExpirationDate = (string)response["expirationDate"],
                ImageURL = data.imageUrl,
                PurchasePrice = totalPrice
            };

            NonQueryResultModel orderResult = customerOrderTable.InsertRecord(customerOrder);

            
            return Json(response);
        }

        //POST: Ebay/OrderApiPlaceGuestOrder/{data}
        public ActionResult OrderApiPlaceGuestOrder(EbayOrderApiPlaceGuestOrderModel data)
        {
            throw new NotImplementedException();
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
            

            /*
            //Testing out Authorization
            return Json(EbayAccessToken.RequestApplicationAccessToken());
            */
        }
    }
}
 