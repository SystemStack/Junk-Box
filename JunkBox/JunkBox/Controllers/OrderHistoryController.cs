using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JunkBox.Models;
using JunkBox.DataAccess;

namespace JunkBox.Controllers
{
    public class OrderHistoryController : Controller
    {
        private CustomerOrderTable customerOrderTable = CustomerOrderTable.Instance();
        private CustomerTable customerTable = CustomerTable.Instance();

        // POST /OrderHistory/GetCustomerOrders/{data}
        [HttpPost]
        public ActionResult GetCustomerOrders(OrderHistoryCustomerDataModel data)
        {
            CustomerResultModel customerResult = customerTable.SelectRecord(new SelectCustomerModel() { Email = data.email });

            List<CustomerOrderResultModel> orderResults = customerOrderTable.SelectAllRecords(new SelectCustomerOrderModel() { CustomerUUID = customerResult.CustomerUUID });

            return Json(orderResults);
        }
    }
}