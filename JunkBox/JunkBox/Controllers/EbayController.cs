using System.Collections.Generic;
using System.Web.Mvc;
using JunkBox.Models;
using JunkBox.Common;
using System.Web.Script.Serialization;

namespace JunkBox.Controllers
{
    public class EbayController : Controller
    {

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
                //{ "SECURITY-APPNAME", appId}, //Auto-included in GetEbayResult()
                { "GLOBAL-ID", "EBAY-US"},
                { "RESPONSE-DATA-FORMAT", "JSON"},
                { "REST-PAYLOAD", ""},
                { "keywords", "harry%20potter"},
                { "paginationInput.entriesPerPage", "3"}
            };

            return Json(Ebay.GetEbayResult(URL, urlParameters));
        }
    }
}