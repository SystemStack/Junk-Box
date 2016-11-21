using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JunkBox.Models;
using JunkBox.DataAccess;
using JunkBox.Common;

namespace JunkBox.Controllers
{
    public class PreferencesController : Controller
    {
        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        //POST: Prefrences/ValidateAddress/{data}
        [HttpPost]
        public ActionResult UpdateAddress(PreferenceAddressModel data)
        {

            List<Dictionary<string, string>> customerData = dataAccess.Select("SELECT CustomerID, AddressID FROM Customer WHERE Email='" + data.email + "'");
            if (customerData.Count <= 0)
            {
                return Json(new { result = "Fail" });
            }

            string customerId = customerData.First()["CustomerID"];
            string addressId = customerData.First()["AddressID"];


            Dictionary<string, string> addressUpdates = new Dictionary<string, string>()
            {
                {"BillingAddress", data.streetName},
                {"BillingAddress2", data.streetName2},
                {"BillingCity", data.city},
                {"BillingZip", data.postalCode},

                {"ShippingAddress", data.streetName},
                {"ShippingAddress2", data.streetName2},
                {"ShippingCity", data.city},
                {"ShippingZip", data.postalCode},
            };
            int result = dataAccess.Update("Address", addressUpdates, "AddressID", addressId);

            if (result == 1)
            {
                return Json(new { result = "Success" });
            }
            else
            {
                return Json(new { result = "Fail" });
            }
        }

        //POST: Preferences/ChangePassword/{data}
        [HttpPost]
        public ActionResult ChangePassword(PreferenceChangePasswordModel data)
        {
            List<Dictionary<string, string>> customerData = dataAccess.Select("SELECT Hash, CustomerID FROM Customer WHERE Email='" + data.email + "'");
            if(customerData.Count <= 0)
            {
                return Json(new { result="Fail"});
            }

            string customerHash = customerData.First()["Hash"];
            string customerId = customerData.First()["CustomerID"];

            bool verifyPassword = Password.VerifyHash(data.oldPassword, customerHash);

            if(!verifyPassword)
            {
                return Json(new { result="Fail"});
            }

            byte[] salt = Password.ComputeSaltBytes();

            string hashString = Password.ComputeHash(data.newPassword, salt);
            string saltString = Convert.ToBase64String(salt);

            Dictionary<string, string> updateParams = new Dictionary<string, string>(){
                {"Hash", hashString},
                {"Salt", saltString}
            };
            int result = dataAccess.Update("Customer", updateParams, "CustomerID", customerId);

            if(result == 0)
            {
                return Json(new { result="Fail"});
            }

            return Json(new { result="Success" });
        }

        //POST: Preferences/HaltPurchases/{data}
        [HttpPost]
        public ActionResult HaltPurchases(PreferenceHaltPurchaseModel data)
        {
            return Json(new { result=data.action});
        }
    }
}