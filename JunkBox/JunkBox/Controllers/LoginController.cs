using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using JunkBox.DataAccess;

using JunkBox.Models;
using JunkBox.Common;

using System.Linq;

namespace JunkBox.Controllers {
    public class LoginController : Controller {

        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        // POST: Login/Login/{data}
        [HttpPost]

        public ActionResult Login (LoginLoginModel id) {

            List<Dictionary<string, string>> userRecord = dataAccess.Select("SELECT Hash, Salt FROM Customer WHERE Email='" + id.email + "'");

            if(userRecord.Count <= 0)
            {
                return Json(new { result="Fail" });
            }

            bool verifyHash = Password.VerifyHash(id.password, userRecord.First()["Hash"]);

            if (verifyHash)
            {
                return Json(new { result="Success" });
            }
            else
            {
                return Json(new { result="Fail" });
            }
        }

        // POST: Login/Register/{data}
        [HttpPost]
        public ActionResult Register (LoginRegisterModel id) {

            //Check if we already have a user registered with the same email address
            if(dataAccess.Select("SELECT CustomerID FROM Customer WHERE Email='" + id.email + "'").Count >= 1)
            {
                return Json(new { result="Fail"});
            }

            //Insert user's address into Address table
            Dictionary<string, string> newUserAddress = new Dictionary<string, string>() {
                {"BillingCity", id.city},
                {"BillingState", id.state},
                {"BillingZip", id.postalCode},
                {"BillingAddress", id.address},
                {"BillingAddress2", id.address2},
                {"ShippingCity", id.city},
                {"ShippingState", id.state},
                {"ShippingZip", id.postalCode},
                {"ShippingAddress", id.address},
                {"ShippingAddress2", id.address2}
            };
            int addressResult = dataAccess.Insert("Address", newUserAddress);

            //Get the AddressID of the record we just inserted
            string addressId = dataAccess.Select("SELECT LAST_INSERT_ID();").First()["LAST_INSERT_ID()"];

            //Insert User's Query preferences into Query table
            Dictionary<string, string> newUserQuery = new Dictionary<string, string>() {
                {"Frequency", "NEVER"},
                {"PriceLimit", "1.00"},
                {"Category", "Default"}
            };
            int queryResult = dataAccess.Insert("Query", newUserQuery);

            //Get the QueryID of the record we just inserted
            string queryId = dataAccess.Select("SELECT LAST_INSERT_ID();").First()["LAST_INSERT_ID()"];


            //Generate Password's Salt and Hash
            byte[] salt = Password.ComputeSaltBytes();
            string hashString = Password.ComputeHash(id.password, salt);
            string saltString = Convert.ToBase64String(salt);


            //Insert user into Customer table
            Dictionary<string, string> newUserDetails = new Dictionary<string, string>() {
                {"QueryID", queryId},
                {"AddressID", addressId},
                {"FirstName", id.firstName},
                {"LastName", id.lastName},
                {"Phone", id.phone},
                {"Hash", hashString},
                {"Salt", saltString},
                {"Email", id.email}
            };
            int customerResult = dataAccess.Insert("Customer", newUserDetails);

            /*
            //Example of gaining some info that we just entered
            List<Dictionary<string, string>> cust = dataAccess.Select("SELECT CustomerID FROM Customer WHERE Email='test@guy.com'");
            string custId = cust.First()["CustomerID"];
            System.Windows.Forms.MessageBox.Show(custId);
            */

            /*
            //Example of delete
            int delete = dataAccess.Delete("Customer", "CustomerID", custId);
            System.Windows.Forms.MessageBox.Show(delete.ToString());
            */

            /*
            //Example of update
            Dictionary<string, string> items = new Dictionary<string, string> {
                {"FirstName", "UpdatedFirstName"},
                {"LastName", "updatedLastNAME"},
                {"Email", "update@testguy.com"}
            };
            int update = dataAccess.Update("Customer", items, "CustomerID", custId);

            */

            /*
            // UPDATE address JOIN customer SET BillingCity = 'Oshkosh' WHERE Address.AddressID = Customer.AddressID = 5
            //Example of update With table Join
            Dictionary<string, string> items = new Dictionary<string, string> {
                {"BillingCity", "Oshkosh"}
            };
            int update = dataAccess.Update("Customer JOIN Address", items, "Address.AddressID", "5");
            */

            return Json(new { result="Success"});
        }
    }
}
