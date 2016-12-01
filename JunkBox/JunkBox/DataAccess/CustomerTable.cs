using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class CustomerTable
    {
        private static IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        public static CustomerUUIDModel GetCustomerUUID(CustomerEmailModel email)
        {
            string query = "SELECT CustomerUUID FROM Customer WHERE Email=@Email;";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@Email", email.Email }
            };

            List<IDictionary<string, object>> customerId = dataAccess.Select(query, parameters);

            string customerUuid = null;
            if(customerId.Count == 1)
            {
                customerUuid = (string)customerId.First()["CustomerUUID"];
            }

            CustomerUUIDModel payload = new CustomerUUIDModel() {
                CustomerUUID = customerUuid
            };
            return payload;
        }

        public static CustomerDataModel GetCustomerData(CustomerUUIDModel customerUuid)
        {
            string query = "SELECT * FROM Customer WHERE CustomerUUID=@CustomerUUID;";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", customerUuid.CustomerUUID }
            };

            IDictionary<string, object> customerData = dataAccess.Select(query, null).First();

            CustomerDataModel payload = new CustomerDataModel() {
                CustomerUUID = (string)customerData["CustomerUUID"],
                FirstName = (string)customerData["FirstName"],
                LastName = (string)customerData["LastName"],
                Phone = (string)customerData["Phone"],
                Hash = (string)customerData["Hash"],
                Salt = (string)customerData["Salt"],
                Email = (string)customerData["Email"]
            };
            return payload;
        }

        public static CustomerHashSaltModel GetCustomerHashSalt(CustomerUUIDModel customerUuid)
        {
            string query = "SELECT Hash, Salt FROM Customer WHERE CustomerUUID=@CustomerUUID;";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", customerUuid.CustomerUUID },
            };

            IDictionary<string, object> customerData = dataAccess.Select(query, parameters).First();

            CustomerHashSaltModel payload = new CustomerHashSaltModel {
                Hash = (string)customerData["Hash"],
                Salt = (string)customerData["Salt"]
            };
            return payload;
        }

        /*
        public static void GetCustomerEmail(CustomerEmailModel email)
        {

        }
        */

        public static CustomerUUIDModel InsertCustomer(InsertCustomerModel customer)
        {
            //53944e06-b698-11e6-9e73-0050569e2378 <-- UUID Example, fine for right now, but should be stored as a Hex value later on
            IDictionary<string, object> uuidResult = dataAccess.Select("SELECT UUID();", null).First();
            string uuid = (string)uuidResult["UUID()"];

            IDictionary<string, object> parameters = new Dictionary<string, object>() {
                { "@CustomerUUID", uuid },
                { "@FirstName", customer.FirstName },
                { "@LastName", customer.LastName },
                { "@Phone", customer.Phone },
                { "@Hash", customer.Hash },
                { "@Salt", customer.Salt },
                { "@Email", customer.Email },
            };

            //INSERT INTO table_name (column1,column2,column3,...) VALUES (value1, value2, value3,...);
            string query = "INSERT INTO Customer (CustomerUUID, FirstName, LastName, Phone, Hash, Salt, Email) VALUES (@CustomerUUID, @FirstName, @LastName, @Phone, @Hash, @Salt, @Email);";

            int result = dataAccess.Insert(query, parameters);

            //If we dont have a successful insert... don't return an actual UUID
            if (result == 0)
            {
                uuid = null;
            }

            CustomerUUIDModel payload = new CustomerUUIDModel() {
                CustomerUUID = uuid
            };

            return payload;
        }
    }
}