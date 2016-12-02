using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using JunkBox.DataAccess;
using JunkBox.Models;

namespace JunkBox.Controllers
{
    public class QueryController : Controller
    {
        private static QueryTable queryTable = QueryTable.Instance();
        private static CustomerTable customerTable = CustomerTable.Instance();

        //POST: Query/GetSettings/{data}
        [HttpPost]
        public ActionResult GetSettings(QueryGetSettingsModel data)
        {
            SelectCustomerModel customerData = new SelectCustomerModel() {
                Email = data.email
            };
            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);

            if(customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            QueryResultModel queryData = queryTable.SelectRecord(new SelectQueryModel() { CustomerUUID = customerResult.CustomerUUID});

            return Json(new { result=queryData });
        }

        //POST: Query/SetSettings/{data}
        [HttpPost]
        public ActionResult SetSettings(QuerySetSettingsModel data)
        {
            SelectCustomerModel customerData = new SelectCustomerModel() {
                Email = data.email
            };
            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);

            UpdateQueryModel queryData = new UpdateQueryModel() {
                CustomerUUID = customerResult.CustomerUUID,
                Category = data.category,
                CategoryID = data.categoryId,
                Frequency = data.frequencyOptions.label,
                PriceLimit = data.price
            };
            NonQueryResultModel updateResult = queryTable.UpdateRecord(queryData);

            if(updateResult.Success)
            {
                return Json(new { result="Success" });
            }
            else
            {
                return Json(new { result="Fail" });
            }
        }
    }
}