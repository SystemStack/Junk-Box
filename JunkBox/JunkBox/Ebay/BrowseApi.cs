using System.Configuration;
using System.Collections.Generic;

using JunkBox.Common;
using JunkBox.Models;

namespace JunkBox.Ebay
{
    public class BrowseAPI
    {
        private static string authToken = ConfigurationManager.AppSettings["AuthToken"];
        private static string baseUrl = "https://api.sandbox.ebay.com";

        public static SearchPagedCollection ItemSummarySearch(string categoryId, string maxPrice)
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

            return Web.Get<SearchPagedCollection>(apiUrl + Web.BuildQueryString(urlParameters));
        }
    }
}