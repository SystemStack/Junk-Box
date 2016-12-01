using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class AddressTable
    {
        private static IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        public static NonQueryResultModel InsertAddress(AddressModel address)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", address.CustomerUUID },
                { "@BillingCity", address.BillingCity },
                { "@BillingState", address.BillingState },
                { "@BillingZip", address.BillingZip },
                { "@BillingAddress", address.BillingAddress },
                { "@BillingAddress2", address.BillingAddress2 },
                { "@ShippingCity", address.ShippingCity },
                { "@ShippingState", address.ShippingState },
                { "@ShippingZip", address.ShippingZip },
                { "@ShippingAddress", address.ShippingAddress },
                { "@ShippingAddress2", address.ShippingAddress2 }
            };

            //INSERT INTO table_name (column1,column2,column3,...) VALUES (value1, value2, value3,...);
            string query = "INSERT INTO Address (CustomerUUID, BillingCity, BillingState, BillingZip, BillingAddress, BillingAddress2, ShippingCity, ShippingState, ShippingZip, ShippingAddress, ShippingAddress2)" +
                                       " VALUES (@CustomerUUID, @BillingCity, @BillingState, @BillingZip, @BillingAddress, @BillingAddress2, @ShippingCity, @ShippingState, @ShippingZip, @ShippingAddress, @ShippingAddress2);";

            int result = dataAccess.Insert(query, parameters);

            bool succeeded = false;
            if(result == 1)
            {
                succeeded = true;
            }

            NonQueryResultModel payload = new NonQueryResultModel() {
                Success = succeeded
            };

            return payload;
        }

        public static AddressModel GetAddress(CustomerUUIDModel customerUuid)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", customerUuid.CustomerUUID }
            };
            string query = "SELECT * FROM Address WHERE CustomerUUID=@CustomerUUID";

            IDictionary<string, object> addressResult = dataAccess.Select(query, parameters).First();

            AddressModel payload = new AddressModel()
            {
                BillingAddress = (string)addressResult["BillingAddress"],
                BillingAddress2 = (string)addressResult["BillingAddress2"],
                BillingCity = (string)addressResult["BillingCity"],
                BillingState = (string)addressResult["BillingState"],
                BillingZip = (int)addressResult["BillingZip"],

                ShippingAddress = (string)addressResult["ShippingAddress"],
                ShippingAddress2 = (string)addressResult["ShippingAddress2"],
                ShippingCity = (string)addressResult["ShippingCity"],
                ShippingState = (string)addressResult["ShippingState"],
                ShippingZip = (int)addressResult["ShippingZip"]
            };

            return payload;
        }

        public static NonQueryResultModel UpdateAddressData(AddressModel address, CustomerUUIDModel customerUuid)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>() {
                { "@CustomerUUID", customerUuid.CustomerUUID },
                { "@BillingCity", address.BillingCity },
                { "@BillingState", address.BillingState },
                { "@BillingZip", address.BillingZip },
                { "@BillingAddress", address.BillingAddress },
                { "@BillingAddress2", address.BillingAddress2 },
                { "@ShippingCity", address.ShippingCity },
                { "@ShippingState", address.ShippingState },
                { "@ShippingZip", address.ShippingZip },
                { "@ShippingAddress", address.ShippingAddress },
                { "@ShippingAddress2", address.ShippingAddress2 }
            };

            //UPDATE table_name SET column1 = value, column2 = value2,... WHERE some_column = some_value
            string query = "UPDATE Address SET BillingCity=@BillingCity, BillingState=@BillingState, BillingZip=@BillingZip, BillingAddress=@BillingAddress, BillingAddress2=@BillingAddress2, ShippingCity=@ShippingCity, ShippingState=@ShippingState, ShippingZip=@ShippingZip, ShippingAddress=@ShippingAddress, ShippingAddress2=@ShippingAddress2 WHERE CustomerUUID=@CustomerUUID;";

            int result = dataAccess.Update(query, parameters);

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