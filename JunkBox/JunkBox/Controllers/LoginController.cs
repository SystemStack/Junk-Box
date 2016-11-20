using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JunkBox.DataAccess;
using System.Data.Common;
using System.Web.Script.Serialization;
using JunkBox.Models;

namespace JunkBox.Controllers {
    public class LoginController : Controller {

        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        // POST: Login/Login/email@site.domain,passW0rd1
        [HttpPost]
        public ActionResult Login (LoginModel id) {

            List<Dictionary<string, string>> userRecord = dataAccess.Select("SELECT Hash, Salt FROM Customer WHERE Email='" + id.email + "'");

            if(userRecord.Count <= 0)
            {
                return Json(new { result="Not Registered" });
            }

            string userSalt = userRecord.First()["Salt"];

            return Json(new { salt=userSalt });
        }

        // POST: Login/Register/{data}
        [HttpPost]
        public ActionResult Register (RegisterModel id) {

            if(dataAccess.Select("SELECT CustomerID FROM Customer WHERE Email='" + id.email + "'").Count >= 1)
            {
                return Json(new { result="Email address already registered"});
            }


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

            Dictionary<string, string> newUserDetails = new Dictionary<string, string>() {
                {"QueryID", ""},
                {"AddressID", addressId},
                {"FirstName", id.firstName},
                {"LastName", id.lastName},
                {"Phone", id.phone},
                {"Hash", id.hash},
                {"Salt", id.salt},
                {"Email", id.email}
            };
            int customerResult = dataAccess.Insert("Customer", newUserDetails);


            /*
            //We create a Dictionary<string, string> object and pass it into dataAccess.Insert
            Dictionary<string, string> parameters = new Dictionary<string, string>() {
                {"QueryID", "3"},
                {"AddressID", "2"},
                {"FirstName", "InsertTest"},
                {"LastName", "IHopeThisWorks"},
                {"Phone", "1112224444"},
                {"Hash", "g"},
                {"Salt", "4"},
                {"Email", "test@guy.com"}
            };
            int result = dataAccess.Insert("Customer", parameters);
            */

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

            return Json(new { result="OK!"});
        }

    }
}
