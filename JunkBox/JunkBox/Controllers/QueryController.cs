using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using JunkBox.DataAccess;
using JunkBox.Models;

namespace JunkBox.Controllers
{
    public class QueryController : Controller
    {
        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        //POST: Query/GetSettings/{data}
        [HttpPost]
        public ActionResult GetSettings(QueryGetSettingsModel data)
        {
            List<Dictionary<string, string>> customerData = dataAccess.Select("SELECT CustomerID, QueryID FROM Customer WHERE Email='" + data.email + "'");
            if(customerData.Count <= 0)
            {
                return Json(new { result="Fail" });
            }

            string queryId = customerData.First()["QueryID"];

            List<Dictionary<string, string>> queryData = dataAccess.Select("SELECT * FROM Query WHERE QueryID='" + queryId + "'");
            if(queryData.Count <= 0)
            {
                return Json(new { result="Fail" });
            }

            return Json(new { result=queryData.First() });
        }

        //POST: Query/SetSettings/{data}
        [HttpPost]
        public ActionResult SetSettings(QuerySetSettingsModel data)
        {
            List<Dictionary<string, string>> customerData = dataAccess.Select("SELECT CustomerID, QueryID FROM Customer WHERE Email='" + data.email + "'");
            if(customerData.Count <= 0)
            {
                return Json(new { result="Fail" });
            }

            string queryId = customerData.First()["QueryID"];
            string customerId = customerData.First()["CustomerID"];

            List<Dictionary<string, string>> queryData = dataAccess.Select("SELECT QueryID FROM Query WHERE QueryID='" + queryId + "'");
            if(queryData.Count <= 0)
            {
                //We have the option to create and insert a new Query record here for the customer
                return Json(new { result="Fail" });
            }

            Dictionary<string, string> queryUpdate = new Dictionary<string, string>() {
                {"Category", data.category},
                {"PriceLimit", data.price},
                {"Frequency", data.frequencyOptions.label},
                {"CategoryID", data.categoryId}
            };
            int updateResult = dataAccess.Update("Query", queryUpdate, "QueryID", queryId);

            if(updateResult <= 0)
            {
                return Json(new { result="Fail" });
            }

            return Json(new { result="Success" });
        }
    }
}