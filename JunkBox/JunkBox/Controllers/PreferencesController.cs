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
        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        //POST: Prefrences/ValidateAddress/{data}
        [HttpPost]
        public ActionResult UpdateAddress(PreferenceAddressModel data)
        {
            CustomerEmailModel customerEmail = new CustomerEmailModel()
            {
                Email = data.email
            };
            CustomerUUIDModel customerUuid = CustomerTable.GetCustomerUUID(customerEmail);

            if (customerUuid.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            AddressModel customerAddress = new AddressModel()
            {
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
            NonQueryResultModel updateResult = AddressTable.UpdateAddressData(customerAddress, customerUuid);

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
            CustomerEmailModel customerEmail = new CustomerEmailModel() {
                Email = data.email
            };
            CustomerUUIDModel customerUuid = CustomerTable.GetCustomerUUID(customerEmail);

            if(customerUuid.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }


            CustomerHashSaltModel customerHashSalt = CustomerTable.GetCustomerHashSalt(customerUuid);

            bool verifyPassword = Password.VerifyHash(data.oldPassword, customerHashSalt.Hash);
            if (!verifyPassword)
            {
                return Json(new { result="Fail", reason="Invalid Password" });
            }

            //Generate Password's Salt and Hash
            byte[] salt = Password.ComputeSaltBytes();
            string hashString = Password.ComputeHash(data.newPassword, salt);
            string saltString = Convert.ToBase64String(salt);

            customerHashSalt.Hash = hashString;
            customerHashSalt.Salt = saltString;
            customerHashSalt.CustomerUUID = customerUuid.CustomerUUID;

            NonQueryResultModel updateResult = CustomerTable.UpdatePassword(customerHashSalt);

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
            CustomerEmailModel customerEmail = new CustomerEmailModel() {
                Email = data.email
            };
            CustomerUUIDModel customerUuid = CustomerTable.GetCustomerUUID(customerEmail);

            if(customerUuid.CustomerUUID == null)
            {
                return Json(new { result="Fail", reason="Invalid Customer" });
            }

            AddressModel customerAddress = AddressTable.GetAddress(customerUuid);

            return Json(new { result=customerAddress });
        }
    }
}