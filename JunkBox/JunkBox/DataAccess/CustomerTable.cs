using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class CustomerTable : DataTable, IDataTable<CustomerResultModel, SelectCustomerModel, CustomerResultModel, InsertCustomerModel, NonQueryResultModel, UpdateCustomerModel, NonQueryResultModel, DeleteCustomerModel>
    {
        private static CustomerTable instance = null;

        public static CustomerTable Instance()
        {
            if (instance == null)
            {
                instance = new CustomerTable();
            }

            return instance;
        }

        public CustomerResultModel SelectRecord(SelectCustomerModel parameters)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            string where = string.Empty;

            if(parameters.CustomerUUID == null)
            {
                where = "Email=@Email;";
                param.Add("@Email", parameters.Email);
            }
            else
            {
                where = "CustomerUUID=@CustomerUUID;";
                param.Add("@CustomerUUID", parameters.CustomerUUID);
            }

            string query = "SELECT * FROM Customer WHERE " + where;

            List<IDictionary<string, object>> customerResult = dataAccess.Select(query, param);

            if(customerResult.Count == 0)
            {
                return new CustomerResultModel();
            }

            IDictionary<string, object> customerData = customerResult.First();

            CustomerResultModel payload = new CustomerResultModel()
            {
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

        public CustomerResultModel InsertRecord(InsertCustomerModel parameters)
        {
            //53944e06-b698-11e6-9e73-0050569e2378 <-- UUID Example, fine for right now, but should be stored as a Hex value later on
            IDictionary<string, object> uuidResult = dataAccess.Select("SELECT UUID();", null).First();
            string uuid = (string)uuidResult["UUID()"];

            IDictionary<string, object> param = new Dictionary<string, object>() {
                { "@CustomerUUID", uuid },
                { "@FirstName", parameters.FirstName },
                { "@LastName", parameters.LastName },
                { "@Phone", parameters.Phone },
                { "@Hash", parameters.Hash },
                { "@Salt", parameters.Salt },
                { "@Email", parameters.Email },
            };

            string query = "INSERT INTO Customer (CustomerUUID, FirstName, LastName, Phone, Hash, Salt, Email) " +
                                         "VALUES (@CustomerUUID, @FirstName, @LastName, @Phone, @Hash, @Salt, @Email);";

            int result = dataAccess.Insert(query, param);

            //If we dont have a successful insert... don't return an actual UUID
            if (result == 0)
            {
                uuid = null;
            }

            CustomerResultModel payload = new CustomerResultModel()
            {
                CustomerUUID = uuid
            };

            return payload;
        }

        public NonQueryResultModel UpdateRecord(UpdateCustomerModel parameters)
        {
            Dictionary<string, object> param = new Dictionary<string, object>() {
                { "@CustomerUUID", parameters.CustomerUUID },
                { "@FirstName", parameters.FirstName },
                { "@LastName", parameters.LastName },
                { "@Phone", parameters.Phone },
                { "@Hash", parameters.Hash },
                { "@Salt", parameters.Salt },
                { "@Email", parameters.Email }
            };

            string query = "UPDATE Customer SET FirstName=@FirstName, LastName=@LastName, Phone=@Phone, Hash=@Hash, Salt=@Salt, Email=@Email WHERE CustomerUUID=@CustomerUUID;";

            int result = dataAccess.Update(query, param);

            return PrepareNonQueryResult(result);
        }

        public NonQueryResultModel DeleteRecord(DeleteCustomerModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID }
            };

            string query = "DELETE FROM Customer WHERE CustomerUUID=@CustomerUUID;";

            int result = dataAccess.Delete(query, param);

            return PrepareNonQueryResult(result);
        }

        public List<CustomerResultModel> SelectAll(SelectCustomerModel parameters)
        {
            throw new NotImplementedException();
        }
    }
}