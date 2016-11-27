using System.Collections.Generic;
using System.Web.Mvc;
using JunkBox.Models;
using JunkBox.Common;
using System.Web.Script.Serialization;
using System.Configuration;

namespace JunkBox.Controllers
{
    public class EbayController : Controller
    {
        private static string appId = ConfigurationManager.AppSettings["AppID"];

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
                { "paginationInput.entriesPerPage", "3"}
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
    }
}
 