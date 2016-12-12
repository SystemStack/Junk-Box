using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using JunkBox.Models;
using JunkBox.DataAccess;
using JunkBox.Common;

namespace JunkBox.Controllers
{
    public class PreferencesController : Controller
    {
        private CustomerTable customerTable = CustomerTable.Instance(MySqlDataAccess.GetDataAccess());
        private AddressTable addressTable = AddressTable.Instance(MySqlDataAccess.GetDataAccess());

        //POST: Prefrences/ValidateAddress/{data}
        [HttpPost]
        public ActionResult UpdateAddress(PreferenceAddressModel data)
        {
            SelectCustomerModel customerData = new SelectCustomerModel()
            {
                Email = data.email
            };
            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);

            if (customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            UpdateAddressModel customerAddress = new UpdateAddressModel()
            {
                CustomerUUID = customerResult.CustomerUUID,

                BillingAddress = data.streetName,
                BillingAddress2 = data.streetName2,
                BillingCity = data.city,
                BillingState = data.state,
                BillingZip = data.postalCode,

                ShippingAddress = data.streetName,
                ShippingAddress2 = data.streetName2,
                ShippingCity = data.city,
                ShippingState = data.state,
                ShippingZip = data.postalCode
            };
            NonQueryResultModel updateResult = addressTable.UpdateRecord(customerAddress);

            if (updateResult.Success)
            {
                return Json(new { result = "Success" });
            }
            else
            {
                return Json(new { result = "Fail", reason="Database Update Failed" });
            }
        }

        //POST: Preferences/ChangePassword/{data}
        [HttpPost]
        public ActionResult ChangePassword(PreferenceChangePasswordModel data)
        {
            SelectCustomerModel customerData = new SelectCustomerModel() {
                Email = data.email
            };
            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);

            if(customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            bool verifyPassword = Password.VerifyHash(data.oldPassword, customerResult.Hash);
            if (!verifyPassword)
            {
                return Json(new { result="Fail", reason="Invalid Password" });
            }

            //Generate Password's Salt and Hash
            byte[] salt = Password.ComputeSaltBytes();
            string hashString = Password.ComputeHash(data.newPassword, salt);
            string saltString = Convert.ToBase64String(salt);

            customerResult.Hash = hashString;
            customerResult.Salt = saltString;

            UpdateCustomerModel customerUpdate = new UpdateCustomerModel() {
                CustomerUUID = customerResult.CustomerUUID,
                Email = customerResult.Email,
                FirstName = customerResult.FirstName,
                LastName = customerResult.LastName,
                Hash = customerResult.Hash,
                Salt = customerResult.Salt,
                Phone = customerResult.Phone
            };

            NonQueryResultModel updateResult = customerTable.UpdateRecord(customerUpdate);

            if(updateResult.Success)
            {
                return Json(new { result="Success" });
            }
            else
            {
                return Json(new { result="Fail", reason="Password was not updated"});
            }
        }

        //POST: Preferences/HaltPurchases/{data}
        [HttpPost]
        public ActionResult HaltPurchases(PreferenceHaltPurchaseModel data)
        {
            return Json(new { result=data.action });
        }

        //POST: Preferences/GetAddress/{data}
        [HttpPost]
        public ActionResult GetAddress(PreferenceGetAddressModel data)
        {
            SelectCustomerModel customerData = new SelectCustomerModel() {
                Email = data.email
            };
            CustomerResultModel customerResult = customerTable.SelectRecord(customerData);

            if(customerResult.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            AddressResultModel customerAddress = addressTable.SelectRecord(new SelectAddressModel() { CustomerUUID = customerResult.CustomerUUID });

            return Json(new { result=customerAddress });
        }
    }
}