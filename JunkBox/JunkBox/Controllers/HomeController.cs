using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using JunkBox.DataAccess;
using System.Security.Cryptography;

using JunkBox.Models;
using System.Linq;

namespace JunkBox.Controllers
{
    public class HomeController : Controller
    {

        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        // POST: Home/GetRecentPurchases/{data}
        [HttpPost]
        public ActionResult GetRecentPurchases (HomeGetRecentPurchaseModel id)
        {
            CustomerEmailModel customerEmail = new CustomerEmailModel() {
                Email = id.email
            };
            CustomerUUIDModel customerUuid = CustomerTable.GetCustomerUUID(customerEmail);

            if(customerUuid.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            List<CustomerOrderDataModel> orderResults = CustomerOrderTable.GetCustomerOrderData(customerUuid);

            return Json(new { result=orderResults });
        }
    }
}