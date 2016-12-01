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


    }
}