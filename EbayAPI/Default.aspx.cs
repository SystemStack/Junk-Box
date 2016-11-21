using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using FindingAPI_WebApp_Sample.com.ebay.developer;

namespace FindingAPI_WebApp_Sample
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_SOAPSearch_Click(object sender, EventArgs e)
        {
            StringBuilder strResult = new StringBuilder();
            try
            {
                CustomFindingService service = new CustomFindingService();
                service.Url = "http://svcs.ebay.com/services/search/FindingService/v1";
                FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();

                // Setting the required proterty value
                request.keywords = txtKeywords.Text.Trim();

                // Setting the pagination 
                
                PaginationInput pagination = new PaginationInput();
                pagination.entriesPerPageSpecified = true;
                pagination.entriesPerPage = 25;
                pagination.pageNumberSpecified = true;
                pagination.pageNumber = 1;
                request.paginationInput = pagination;
                
                request.sortOrderSpecified = true;
                request.sortOrder = SortOrderType.PricePlusShippingLowest;
                request.outputSelector = new OutputSelectorType[] { OutputSelectorType.SellerInfo};
                ItemFilter filterMin = new ItemFilter();
                ItemFilter filterMax = new ItemFilter();
                filterMin.name =ItemFilterType.MinPrice;
                string[] min = new string [1];
                min[0] = "0";
                filterMin.value= min;

                filterMax.name = ItemFilterType.MaxPrice;
                string[] max = new string[1];
                max[0] = "1.00";
                filterMax.value = max;
                ItemFilter[] filters = new ItemFilter[2];
                filters[0] = filterMin;
                filters[1] = filterMax;

                request.itemFilter = filters;
                


                // Sorting the result
                FindItemsByKeywordsResponse response = service.findItemsByKeywords(request);
                if (response.searchResult.count > 0)
                {
                    foreach (SearchItem searchItem in response.searchResult.item)
                    {
                        strResult.AppendLine("ItemID: " + searchItem.itemId);
                        strResult.AppendLine("Title: " + searchItem.title);
                        strResult.AppendLine("Type: " + searchItem.listingInfo.listingType);                        
                        strResult.AppendLine("Price: " + searchItem.sellingStatus.currentPrice.Value);
                        strResult.AppendLine("Picture: " + searchItem.galleryURL);
                        strResult.AppendLine("------------------------------------------------------------------------");
                    }
                }
                else
                {
                    strResult.AppendLine("No result found...Try with other keyword(s)");
                }
                txtResult.Text = "";
                txtResult.Text = strResult.ToString();
                Response.Write("Total Pages: " + response.paginationOutput.totalPages);
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }        
    }
}
