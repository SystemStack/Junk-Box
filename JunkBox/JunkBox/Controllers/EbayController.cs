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

            return Json(Ebay.Categories.GetEbayResult(URL, urlParameters));
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

            CheckoutSessionResponse response = OrderAPI.InitiateGuestCheckoutSession(data.orderId, customerResult, addressData);

            InsertCustomerOrderModel customerOrder = new InsertCustomerOrderModel() {
                CustomerUUID = customerResult.CustomerUUID,
                CheckoutSessionID = response.checkoutSessionId,
                ExpirationDate = response.expirationDate,
                ImageURL = data.imageUrl,
                PurchasePrice = response.pricingSummary.total.value,
                Title = response.lineItems[0].title
            };

            NonQueryResultModel orderResult = customerOrderTable.InsertRecord(customerOrder);

            return Json(response);
        }

        //POST: Ebay/OrderApiPlaceGuestOrder/{data}
        [HttpPost]
        public ActionResult OrderApiPlaceGuestOrder(EbayOrderApiPlaceGuestOrderModel data)
        {
            PurchaseOrderSummary summary = OrderAPI.PlaceGuestOrder(data.checkoutSessionId);

            return Json(summary);
        }

        //POST: Ebay/DailyPurchase
        [HttpPost]
        public ActionResult DailyPurchase()
        {
            List<CustomerResultModel> customerList = customerTable.SelectAllRecords();

            foreach(CustomerResultModel customer in customerList)
            {
                //Get customer's query prefrences
                QueryResultModel queryPref = queryTable.SelectRecord(new SelectQueryModel() { CustomerUUID = customer.CustomerUUID });

                //Ask Ebay for items.
                SearchPagedCollection itemCollection = BrowseAPI.ItemSummarySearch(queryPref.CategoryID, queryPref.PriceLimit);

                try
                {
                    //There were no items found for this query
                    if (itemCollection.itemSummaries.Length == 0)
                        continue;

                    //Get customer's address data
                    AddressResultModel customerAddress = addressTable.SelectRecord(new SelectAddressModel() { CustomerUUID = customer.CustomerUUID });

                    //Initiate order with a randomly chosen index from itemCollection's itemSummaries
                    Random rand = new Random();
                    int itemIndex = rand.Next(itemCollection.itemSummaries.Length);

                    CheckoutSessionResponse response = OrderAPI.InitiateGuestCheckoutSession(itemCollection.itemSummaries[itemIndex].itemId, customer, customerAddress);

                    InsertCustomerOrderModel customerOrder = new InsertCustomerOrderModel()
                    {
                        CustomerUUID = customer.CustomerUUID,
                        CheckoutSessionID = response.checkoutSessionId,
                        ExpirationDate = response.expirationDate,
                        ImageURL = itemCollection.itemSummaries[itemIndex].image.imageUrl,
                        PurchasePrice = response.pricingSummary.total.value,
                        Title = response.lineItems[0].title
                    };

                    NonQueryResultModel orderResult = customerOrderTable.InsertRecord(customerOrder);
                }
                catch(Exception e)
                {

                }
            }

            return Json(new { result="Daily Purchases Complete" });
        }
    }
}
 