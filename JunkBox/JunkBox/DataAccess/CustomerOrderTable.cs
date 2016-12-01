using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class CustomerOrderTable
    {
        private static IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        public static List<CustomerOrderDataModel> GetCustomerOrderData(CustomerUUIDModel customerUuid)
        {
            string query = "SELECT * FROM CustomerOrder WHERE CustomerUUID=@CustomerUUID;";

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", customerUuid.CustomerUUID },
            };

            List<IDictionary<string, object>> customerData = dataAccess.Select(query, parameters); //Get all the Database entries

            List<CustomerOrderDataModel> payload = new List<CustomerOrderDataModel>(); //Store them in the payload model
            foreach(IDictionary<string, object> entry in customerData)
            {
                CustomerOrderDataModel order = new CustomerOrderDataModel() {
                    CheckoutSessionID = (string)entry["CheckoutSessionID"],
                    ExpirationDate = (string)entry["ExpirationDate"],
                    PurchasePrice = (string)entry["PurchasePrice"],
                    ImageURL = (string)entry["ImageURL"],
                    TimeStamp = (DateTime)entry["TimeStamp"]
                };
                payload.Add(order);
            }

            return payload;
        }

        public static NonQueryResultModel InsertCustomerOrder(CustomerOrderModel customerOrder)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", customerOrder.CustomerUUID },
                { "@CheckoutSessionID", customerOrder.CheckoutSessionID },
                { "@ExpirationDate", customerOrder.ExpirationDate },
                { "@ImageURL", customerOrder.ImageURL },
                { "@PurchasePrice", customerOrder.PurchasePrice }
            };

            //INSERT INTO table_name (column1,column2,column3,...) VALUES (value1, value2, value3,...);
            string query = "INSERT INTO CustomerOrder (CustomerUUID, CheckoutSessionID, ExpirationDate, ImageURL, PurchasePrice)" +
                                       " VALUES (@CustomerUUID, @CheckoutSessionID, @ExpirationDate, @ImageURL, @PurchasePrice);";

            int result = dataAccess.Insert(query, parameters);

            bool succeeded = false;
            if (result == 1)
            {
                succeeded = true;
            }

            NonQueryResultModel payload = new NonQueryResultModel()
            {
                Success = succeeded
            };

            return payload;
        }
    }
}