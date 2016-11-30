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
            CustomerEmailModel customerEmail = new CustomerEmailModel() {
                Email = data.email
            };
            CustomerUUIDModel customerUuid = CustomerTable.GetCustomerUUID(customerEmail);

            if(customerUuid.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            QueryDataModel queryData = QueryTable.GetQueryData(customerUuid);

            return Json(new { result=queryData });
        }

        //POST: Query/SetSettings/{data}
        [HttpPost]
        public ActionResult SetSettings(QuerySetSettingsModel data)
        {
            CustomerEmailModel customerEmail = new CustomerEmailModel() {
                Email = data.email
            };
            CustomerUUIDModel customerUuid = CustomerTable.GetCustomerUUID(customerEmail);

            QueryDataModel queryData = new QueryDataModel() {
                Category = data.category,
                CategoryID = data.categoryId,
                Frequency = data.frequencyOptions.label,
                PriceLimit = data.price
            };
            NonQueryResultModel updateResult = QueryTable.UpdateQueryData(queryData, customerUuid);

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