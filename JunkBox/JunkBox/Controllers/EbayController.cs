using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using System.Linq;

using JunkBox.DataAccess;
using JunkBox.Models;
using JunkBox.Common;

namespace JunkBox.Controllers
{
    public class EbayController : Controller
    {
        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        private static string appId = ConfigurationManager.AppSettings["AppID"];
        private static string appIdSandbox = ConfigurationManager.AppSettings["AppIDSandBox"];

        //POST: Ebay/GetSomething/{data}
        [HttpPost]
        public ActionResult GetSomething(EbayGetSomethingModel data)
        {
            return Json(new { result=Ebay.GetTimestamp() });
        }

        //POST: Ebay/GetTest/
        [HttpPost]
        public ActionResult GetTest()
        {
            string URL = "http://svcs.ebay.com/services/search/FindingService/v1";
            
            /*
            string urlParameters = "?OPERATION-NAME=findItemsByKeywords" +
                                    "&SERVICE-VERSION=1.0.0" + 
                                    "&SECURITY-APPNAME=WalterWo-JunkBox-PRD-e45f6444c-265c3eca" + 
                                    "&GLOBAL-ID=EBAY-US" + 
                                    "&RESPONSE-DATA-FORMAT=JSON" + 
                                    "&REST-PAYLOAD" + 
                                    "&keywords=harry%20potter" + 
                                    "&paginationInput.entriesPerPage=3";
            */
            Dictionary<string, string> urlParameters = new Dictionary<string, string>() {
                { "OPERATION-NAME", "findItemsByKeywords"},
                { "SERVICE-VERSION", "1.0.0"},
                { "SECURITY-APPNAME", appId},
                { "GLOBAL-ID", "EBAY-US"},
                { "RESPONSE-DATA-FORMAT", "JSON"},
                { "REST-PAYLOAD", ""},
                { "keywords", "harry%20potter"},
                { "paginationInput.entriesPerPage", "3"},
                { "itemFilter(0).name", "MaxPrice"},
                { "itemFilter(0).value", "5.00"}
            };

            /*
             * var url2 = "http://svcs.ebay.com/services/search/FindingService/v1?" +
                "OPERATION-NAME=findItemsAdvanced&" +
                "SERVICE-VERSION=1.9.0&"
                "SECURITY-APPNAME=WalterWo-JunkBox-PRD-e45f6444c-265c3eca&" +
                "RESPONSE-DATA-FORMAT=JSON&" +
                "REST-PAYLOAD&" +
                "keywords=nikon+d5000+digital+slr+camera&" +
                "itemFilter(0).name=Condition&" +
                "itemFilter(0).value=New&" +
                "itemFilter(1).name=MaxPrice&" +
                "itemFilter(1).value=750.00&" +
                "itemFilter(1).paramName=Currency&" +
                "itemFilter(1).paramValue=USD&" +
                "itemFilter(2).name=TopRatedSellerOnly&" +
                "itemFilter(2).value=true&" +
                "itemFilter(3).name=ReturnsAcceptedOnly&" +
                "itemFilter(3).value=true&" +
                "itemFilter(4).name=ExpeditedShippingType&" +
                "itemFilter(4).value=Expedited&" +
                "itemFilter(5).name=MaxHandlingTime&" +
                "itemFilter(5).value=1";
            */

            return Json(Ebay.GetEbayResult(URL, urlParameters));
        }

        //POST: Ebay/GetViablePurchases/{data}
        [HttpPost]
        public ActionResult GetViablePurchases(EbayGetViablePurchasesModel data)
        {
            //Get customer info
            Dictionary<string, string> customerInfo = dataAccess.Select("SELECT CustomerID, QueryID FROM Customer WHERE Email='" + data.email + "'").First();

            //Get customer query prefrences 
            Dictionary<string, string> queryPref = dataAccess.Select("SELECT * FROM Query WHERE QueryID='" + customerInfo["QueryID"] + "'").First();

            //Build data required for Ebay API call
            string URL = "http://svcs.ebay.com/services/search/FindingService/v1";
            Dictionary<string, string> urlParameters = new Dictionary<string, string>() {
                { "OPERATION-NAME", "findItemsByCategory"},
                { "SERVICE-VERSION", "1.0.0"},
                { "SECURITY-APPNAME", appId},
                { "GLOBAL-ID", "EBAY-US"},
                { "RESPONSE-DATA-FORMAT", "JSON"},
                { "REST-PAYLOAD", ""},
                { "categoryId", queryPref["CategoryID"]},
                { "paginationInput.entriesPerPage", "4"},
                { "itemFilter(0).name", "MaxPrice"},
                { "itemFilter(0).value", queryPref["PriceLimit"]},
                { "itemFilter(1).name", "ListingType"},
                { "itemFilter(1).value", "AuctionWithBIN"}

            };
            return Json(Ebay.GetEbayResult(URL, urlParameters)); //Aaaaaand done.
        }

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

        //POST: Ebay/BrowseAPITest/
        [HttpPost]
        public ActionResult BrowseAPITest(EbayBrowseAPIModel data)
        {
            //Get customer info
            Dictionary<string, string> customerInfo = dataAccess.Select("SELECT CustomerID, QueryID FROM Customer WHERE Email='" + data.email + "'").First();

            //Get customer query prefrences 
            Dictionary<string, string> queryPref = dataAccess.Select("SELECT * FROM Query WHERE QueryID='" + customerInfo["QueryID"] + "'").First();


            return Json(EbayBrowseAPI.ItemSummarySearch(queryPref["CategoryID"], queryPref["PriceLimit"]));
        }
    }
}
 