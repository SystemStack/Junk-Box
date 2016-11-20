using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using JunkBox.DataAccess;

using System.Data.Common;
using System.Web.Script.Serialization;
using JunkBox.Models;
using System.Security.Cryptography;
using System.Text;

namespace JunkBox.Controllers {
    public class LoginController : Controller {

        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        // POST: Login/Login/{data}
        [HttpPost]

        public ActionResult Login (LoginModel id) {

            List<Dictionary<string, string>> userRecord = dataAccess.Select("SELECT Hash, Salt FROM Customer WHERE Email='" + id.email + "'");

            if(userRecord.Count <= 0)
            {
                return Json(new { result="Fail" });
            }

            bool verifyHash = LoginController.VerifyHash(id.password, userRecord.First()["Hash"]);

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
        public ActionResult Register (RegisterModel id) {

            if(dataAccess.Select("SELECT CustomerID FROM Customer WHERE Email='" + id.email + "'").Count >= 1)
            {
                return Json(new { result="Fail"});
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

            byte[] salt = LoginController.ComputeSaltBytes();

            string hashString = LoginController.ComputeHash(id.password, salt);
            string saltString = Convert.ToBase64String(salt);


            Dictionary<string, string> newUserDetails = new Dictionary<string, string>() {
                {"QueryID", ""},
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

        private static byte[] ComputeSaltBytes()
        {
            byte[] saltBytes;

            int minSaltSize = 4;
            int maxSaltSize = 8;

            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            saltBytes = new byte[saltSize];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(saltBytes);

            return saltBytes;
        }

        private static string ComputeHash(string plainText, byte[] saltBytes)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash = new SHA512Managed();

            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        private static bool VerifyHash(string plainText, string hashValue)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits = 512,
                hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString = LoginController.ComputeHash(plainText, saltBytes);

            return (hashValue == expectedHashString);
        }
    }
}
