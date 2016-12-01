using System;
using System.Web.Mvc;
using JunkBox.DataAccess;

using JunkBox.Models;
using JunkBox.Common;

namespace JunkBox.Controllers
{
    public class LoginController : Controller
    {

        //private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();
        private static QueryTable queryTable = QueryTable.Instance();

        // POST: Login/Login/{data}
        [HttpPost]
        public ActionResult Login (LoginLoginModel id)
        {

            //Get the customer's UUID, preferably in the future, instead of passing email addresses in,
            //We'll pass the UUID, or an Access Token
            CustomerEmailModel customerEmail = new CustomerEmailModel() {
                Email = id.email
            };
            CustomerUUIDModel customerUuid = CustomerTable.GetCustomerUUID(customerEmail);
            //Check to see if the customer exists
            if(customerUuid.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Credentials" });
            }

            //Get their hash and salt
            CustomerHashSaltModel customerHashSalt = CustomerTable.GetCustomerHashSalt(customerUuid);

            //Verify and report accordingly
            bool verifyHash = Password.VerifyHash(id.password, customerHashSalt.Hash);
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
            if (CustomerTable.GetCustomerUUID(new CustomerEmailModel() { Email = id.email }).CustomerUUID != null)
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
            CustomerUUIDModel customerUuid = CustomerTable.InsertCustomer(newCustomer);

            //If it didn't insert, then we won't get a UUID back
            if(customerUuid.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Insert into the database was not successful" });
            }

            //Insert customer's address into the address table
            AddressModel customerAddress = new AddressModel() {
                CustomerUUID = customerUuid.CustomerUUID,

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
           
            NonQueryResultModel addressResult = AddressTable.InsertAddress(customerAddress); //We have the option to 'do something' if the insert fails

            //Insert into Query table
            InsertQueryModel customerQuery = new InsertQueryModel() {
                    CustomerUUID = customerUuid.CustomerUUID,

                    Category = "",
                    CategoryID = "",
                    Frequency = "",
                    PriceLimit = ""
            };
            //NonQueryResultModel queryResult = QueryTable.InsertQuery(customerQuery); //If this fails, we have the option of doing something
            NonQueryResultModel queryResult = queryTable.InsertQuery(customerQuery);

            //Aaaand we're done.
            return Json(new { result="Success"});
        }
    }
}
