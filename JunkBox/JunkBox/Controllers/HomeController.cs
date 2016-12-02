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
        private CustomerTable customerTable = CustomerTable.Instance();
        private CustomerOrderTable customerOrderTable = CustomerOrderTable.Instance();

        // POST: Home/GetRecentPurchases/{data}
        [HttpPost]
        public ActionResult GetRecentPurchases (HomeGetRecentPurchaseModel id)
        {
            SelectCustomerModel customerData = new SelectCustomerModel() {
                Email = id.email
            };
            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);

            if(customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            SelectCustomerOrderModel customerOrderData = new SelectCustomerOrderModel() {
                CustomerUUID = customerResult.CustomerUUID
            };
            List<CustomerOrderResultModel> orderResults = customerOrderTable.SelectAllRecords(customerOrderData);

            return Json(new { result=orderResults });
        }
    }
}