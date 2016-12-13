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

        private CustomerOrderTable(IDataAccess access) : base (access)
        {

        }

        public static CustomerOrderTable Instance(IDataAccess access)
        {
            if (instance == null)
            {
                instance = new CustomerOrderTable(access);
            }

            return instance;
        }

        public CustomerOrderResultModel SelectRecord(SelectCustomerOrderModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@OrderID", parameters.OrderID }
            };

            string query = "SELECT * FROM CustomerOrder WHERE OrderID=@OrderID";

            List<IDictionary<string, object>> selectResult = dataAccess.Select(query, param);
            
            if(selectResult.Count == 0)
            {
                return new CustomerOrderResultModel();
            }

            IDictionary<string, object> result = selectResult.First();

            CustomerOrderResultModel payload = new CustomerOrderResultModel() {
                CheckoutSessionID = (string)result["CheckoutSessionID"],
                CustomerUUID = (string)result["CustomerUUID"],
                ExpirationDate = (string)result["ExpirationDate"],
                ImageURL = (string)result["ImageURL"],
                OrderID = (int)result["OrderID"],
                PurchasePrice = (string)result["PurchasePrice"],
                TimeStamp = (DateTime)result["TimeStamp"],
                Title = (string)result["Title"]
            };

            return payload;
        }

        public NonQueryResultModel InsertRecord(InsertCustomerOrderModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID },
                { "@CheckoutSessionID", parameters.CheckoutSessionID },
                { "@ExpirationDate", parameters.ExpirationDate },
                { "@ImageURL", parameters.ImageURL },
                { "@PurchasePrice", parameters.PurchasePrice },
                {"@Title", parameters.Title }
            };

            string query = "INSERT INTO CustomerOrder (CustomerUUID, CheckoutSessionID, ExpirationDate, ImageURL, PurchasePrice, Title)" +
                                       " VALUES (@CustomerUUID, @CheckoutSessionID, @ExpirationDate, @ImageURL, @PurchasePrice, @Title);";

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

            string query = "SELECT * FROM CustomerOrder WHERE CustomerUUID=@CustomerUUID ORDER BY TimeStamp DESC;";

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
                    TimeStamp = (DateTime)entry["TimeStamp"],
                    Title = (string)entry["Title"]
                };
                payload.Add(order);
            }

            return payload;
        }

    }
}