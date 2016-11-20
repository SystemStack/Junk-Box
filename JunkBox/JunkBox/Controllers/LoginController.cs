using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using JunkBox.DataAccess;
using System.Security.Cryptography;

namespace JunkBox.Controllers {
    public class LoginController : Controller {

        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        // POST: Login/Login/email@site.domain,passW0rd1
        [HttpPost]
        public String Login (String id) {
            id = id.Replace("PERIODHERE", ".");
            String password = id.Split(',')[1];
            //return password;
            //System.Windows.Forms.MessageBox.Show(id);

            //Give the Select function a Select Command
            //Results will be populated inside a list of Key/Value pairs
            List<Dictionary<string, string>> results = dataAccess.Select("SELECT * FROM Customer");

            //This is how we can iterate over that list
            foreach (Dictionary<string, string> row in results) {
                //We have a lot of data contained in the dictionary item
                //string keys = "";
                //string values = "";

                //We can iterate over every key/value pair
                //foreach(KeyValuePair<string, string> item in row)
                //{
                //    keys += " " + item.Key;
                //    values += " " + item.Value;
                //}
                //System.Windows.Forms.MessageBox.Show("KEYS: " + keys + " VALUES: " + values);


                //Or we can just access the specific data we want
                System.Windows.Forms.MessageBox.Show("FirstName: " + row["FirstName"].ToString());
            }

            return HttpUtility.UrlDecode(id).ToString();
        }

        // POST: Login/Login/email@site.domain,passW0rd1
        [HttpPost]
        public String Register (String id) {
            dataAccess.OpenConnection();
            //dataAccess.insert("INSERT INTO `cs341_t5`.`Customer` (`CustomerID`, `QueryID`, `AddressID`, `FirstName`, `LastName`, `Phone`, `Hash`, `Salt`, `Email`) VALUES (NULL, '3', '3', 'REGISTER_TEST', 'Test', '1112223333', '4', 'r', 'walter@test.com');");

            //dataAccess.CloseConnection();

            /*
            id = id.Replace("PERIODHERE", ".");
            String password = id.Split(',')[1];
            //return password;
            */


            RandomNumberGenerator saltGenerator = RandomNumberGenerator.Create();
            HashAlgorithm hasher = new SHA512Managed();
            Byte[] saltArr = new Byte[32];
            saltGenerator.GetBytes(saltArr);
            String salt = System.Text.Encoding.Default.GetString(saltArr);
            Byte[] hashResult = hasher.Hash;


            Dictionary<string, string> addressInsertObject = new Dictionary<string, string>() {
                {"BillingCity", "info"},
                {"BillingState", "info" },
                {"BillingZip", "info"},
                {"BillingAddress", "info"},
                {"BillingAddress2", "info"},
                {"ShippingCity", "info"},
                {"ShippingState", "info" },
                {"ShippingZip", "info"},
                {"ShippingAddress", "info"},
                {"ShippingAddress2", "info"},

            };
            int addressInsertResult = dataAccess.Insert("Address", addressInsertObject);
            //int addressID = addressInsertResult.
            Dictionary<string, string> customerInsertObject = new Dictionary<string, string>() {
                {"QueryID", "-1"},
                {"AddressID", addressInsertResult.ToString()},
                {"FirstName", "InsertTest"},
                {"LastName", "IHopeThisWorks"},
                {"Phone", "1112224444"},
                {"Hash", "g"},
                {"Salt", "4"},
                {"Email", "test@guy.com"}
            };

            int customerInsertResult = dataAccess.Insert("Customer", customerInsertObject);


            /*

            //Example of gaining some info that we just entered
            List<Dictionary<string, string>> cust = dataAccess.Select("SELECT CustomerID FROM Customer WHERE Email='test@guy.com'");
            string custId = cust.First()["CustomerID"];
            System.Windows.Forms.MessageBox.Show(custId);

            /*
            //Example of delete
            int delete = dataAccess.Delete("Customer", "CustomerID", custId);
            System.Windows.Forms.MessageBox.Show(delete.ToString());

            

            //Example of update
            Dictionary<string, string> items = new Dictionary<string, string> {
                {"FirstName", "UpdatedFirstName"},
                {"LastName", "updatedLastNAME"},
                {"Email", "update@testguy.com"}
            };
            int update = dataAccess.Update("Customer", items, "CustomerID", custId);
            
            */

            //Example of update With table Join
            Dictionary<string, string> items = new Dictionary<string, string> {
                {"BillingCity", "Oshkosh"}
            };
            int update = dataAccess.Update("Customer JOIN Address", items, "Address.AddressID", "5");

            //UPDATE address JOIN customer SET BillingCity='Oshkosh' WHERE Address.AddressID = Customer.AddressID = 5

            return HttpUtility.UrlDecode(id).ToString();
        }

    }
}
