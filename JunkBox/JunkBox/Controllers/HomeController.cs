using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using JunkBox.DataAccess;
using System.Security.Cryptography;

using JunkBox.Models;
using System.Linq;

namespace JunkBox.Controllers {
    public class HomeController : Controller {

        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        // POST: Home/.....
        [HttpPost]
        public ActionResult GetRecentPurchases (GetRecentPurchaseModel id) {

            List<Dictionary<string, string>> results = dataAccess.Select("SELECT CustomerID FROM Customer WHERE Email='" + id.email + "'");
            
            if(results.Count <= 0)
            {
                return Json(new { result="Fail"});
            }
            string customerId = results.First()["CustomerID"];

            List<Dictionary<string, string>> purchaseResults = dataAccess.Select("SELECT * FROM CustomerOrder WHERE CustomerID='" + customerId + "'");

            return Json(new { result=purchaseResults });
        }
    }
}