using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class CustomerOrderTable : DataTable, IDataTable<CustomerOrderResultModel, SelectCustomerOrderModel, NonQueryResultModel, InsertCustomerOrderModel, NonQueryResultModel, UpdateCustomerOrderModel, NonQueryResultModel, DeleteCustomerOrderModel>
    {
        private static CustomerOrderTable instance = null;

        public static CustomerOrderTable Instance()
        {
            if (instance == null)
            {
                instance = new CustomerOrderTable();
            }

            return instance;
        }

        public CustomerOrderResultModel SelectRecord(SelectCustomerOrderModel parameters)
        {
            throw new NotImplementedException();
        }

        public NonQueryResultModel InsertRecord(InsertCustomerOrderModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID },
                { "@CheckoutSessionID", parameters.CheckoutSessionID },
                { "@ExpirationDate", parameters.ExpirationDate },
                { "@ImageURL", parameters.ImageURL },
                { "@PurchasePrice", parameters.PurchasePrice }
            };

            string query = "INSERT INTO CustomerOrder (CustomerUUID, CheckoutSessionID, ExpirationDate, ImageURL, PurchasePrice)" +
                                       " VALUES (@CustomerUUID, @CheckoutSessionID, @ExpirationDate, @ImageURL, @PurchasePrice);";

            int result = dataAccess.Insert(query, param);

            return PrepareNonQueryResult(result);
        }

        public NonQueryResultModel UpdateRecord(UpdateCustomerOrderModel parameters)
        {
            throw new NotImplementedException();
        }

        public NonQueryResultModel DeleteRecord(DeleteCustomerOrderModel parameters)
        {
            throw new NotImplementedException();
        }

        public List<CustomerOrderResultModel> SelectAllRecords(SelectCustomerOrderModel parameters)
        {
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID },
            };

            string query = "SELECT * FROM CustomerOrder WHERE CustomerUUID=@CustomerUUID;";

            List<IDictionary<string, object>> customerData = dataAccess.Select(query, param); //Get all the Database entries

            List<CustomerOrderResultModel> payload = new List<CustomerOrderResultModel>(); //Store them in the payload model
            foreach (IDictionary<string, object> entry in customerData)
            {
                CustomerOrderResultModel order = new CustomerOrderResultModel()
                {
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