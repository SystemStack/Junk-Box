using System;
using System.Web.Mvc;
using JunkBox.DataAccess;

using JunkBox.Models;
using JunkBox.Common;

namespace JunkBox.Controllers
{
    public class LoginController : Controller
    {
        private static QueryTable queryTable = QueryTable.Instance();
        private static CustomerTable customerTable = CustomerTable.Instance();
        private static AddressTable addressTable = AddressTable.Instance();

        // POST: Login/Login/{data}
        [HttpPost]
        public ActionResult Login (LoginLoginModel id)
        {

            //Get the customer's UUID, preferably in the future, instead of passing email addresses in,
            //We'll pass the UUID, or an Access Token
            SelectCustomerModel customerData = new SelectCustomerModel() {
                Email = id.email
            };
            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);
            //Check to see if the customer exists
            if(customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Credentials" });
            }

            //Verify and report accordingly
            bool verifyHash = Password.VerifyHash(id.password, customerResult.Hash);
            if (verifyHash)
            {
                return Json(new { result="Success" });
            }
            else
            {
                return Json(new { result="Fail", reason="Invalid Credentials" });
            }
        }

        // POST: Login/Register/{data}
        [HttpPost]
        public ActionResult Register (LoginRegisterModel id)
        {

            //Check if we already have a user registered with the same email address
            if (customerTable.SelectRecord(new SelectCustomerModel() { Email = id.email }).CustomerUUID != null)
            {
                return Json(new { result="Fail", reason="Email address is already registered" });
            }

            //Generate Password's Salt and Hash
            byte[] salt = Password.ComputeSaltBytes();
            string hashString = Password.ComputeHash(id.password, salt);
            string saltString = Convert.ToBase64String(salt);

            //Insert into Customer table
            InsertCustomerModel newCustomer = new InsertCustomerModel() {
                FirstName = id.firstName,
                LastName = id.lastName,
                Phone = id.phone,
                Email = id.email,
                Hash = hashString,
                Salt = saltString
            };
            CustomerResultModel customerResult = customerTable.InsertRecord(newCustomer);

            //If it didn't insert, then we won't get a UUID back
            if(customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Insert into the database was not successful" });
            }

            //Insert customer's address into the address table
            InsertAddressModel customerAddress = new InsertAddressModel() {
                CustomerUUID = customerResult.CustomerUUID,

                BillingAddress = id.address,
                BillingAddress2 = id.address2,
                BillingCity = id.city,
                BillingState = id.state,
                BillingZip = Int32.Parse(id.postalCode),

                ShippingAddress = id.address,
                ShippingAddress2 = id.address2,
                ShippingCity = id.city,
                ShippingState = id.state,
                ShippingZip = Int32.Parse(id.postalCode)
            };
           
            NonQueryResultModel addressResult = addressTable.InsertRecord(customerAddress); //We have the option to 'do something' if the insert fails

            //Insert into Query table
            InsertQueryModel customerQuery = new InsertQueryModel() {
                    CustomerUUID = customerResult.CustomerUUID,

                    Category = "",
                    CategoryID = "",
                    Frequency = "",
                    PriceLimit = ""
            }; 
            NonQueryResultModel queryResult = queryTable.InsertRecord(customerQuery); //If this fails, we have the option of doing something

            //Aaaand we're done.
            return Json(new { result="Success"});
        }
    }
}
