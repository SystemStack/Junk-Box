using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class AddressTable : DataTable, IDataTable<AddressResultModel, SelectAddressModel, NonQueryResultModel, InsertAddressModel, NonQueryResultModel, UpdateAddressModel, NonQueryResultModel, DeleteAddressModel>
    {
        private static AddressTable instance = null;

        private AddressTable(IDataAccess access) : base(access)
        {

        }

        public static AddressTable Instance(IDataAccess access)
        {
            if (instance == null)
            {
                instance = new AddressTable(access);
            }

            return instance;
        }

        public AddressResultModel SelectRecord(SelectAddressModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID }
            };
            string query = "SELECT * FROM Address WHERE CustomerUUID=@CustomerUUID";

            IDictionary<string, object> addressResult = dataAccess.Select(query, param).First();

            AddressResultModel payload = new AddressResultModel()
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

        public NonQueryResultModel InsertRecord(InsertAddressModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID },
                { "@BillingCity", parameters.BillingCity },
                { "@BillingState", parameters.BillingState },
                { "@BillingZip", parameters.BillingZip },
                { "@BillingAddress", parameters.BillingAddress },
                { "@BillingAddress2", parameters.BillingAddress2 },
                { "@ShippingCity", parameters.ShippingCity },
                { "@ShippingState", parameters.ShippingState },
                { "@ShippingZip", parameters.ShippingZip },
                { "@ShippingAddress", parameters.ShippingAddress },
                { "@ShippingAddress2", parameters.ShippingAddress2 }
            };

            //INSERT INTO table_name (column1,column2,column3,...) VALUES (value1, value2, value3,...);
            string query = "INSERT INTO Address (CustomerUUID, BillingCity, BillingState, BillingZip, BillingAddress, BillingAddress2, ShippingCity, ShippingState, ShippingZip, ShippingAddress, ShippingAddress2)" +
                                       " VALUES (@CustomerUUID, @BillingCity, @BillingState, @BillingZip, @BillingAddress, @BillingAddress2, @ShippingCity, @ShippingState, @ShippingZip, @ShippingAddress, @ShippingAddress2);";

            int result = dataAccess.Insert(query, param);

            return PrepareNonQueryResult(result);
        }

        public NonQueryResultModel UpdateRecord(UpdateAddressModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>() {
                { "@CustomerUUID", parameters.CustomerUUID },
                { "@BillingCity", parameters.BillingCity },
                { "@BillingState", parameters.BillingState },
                { "@BillingZip", parameters.BillingZip },
                { "@BillingAddress", parameters.BillingAddress },
                { "@BillingAddress2", parameters.BillingAddress2 },
                { "@ShippingCity", parameters.ShippingCity },
                { "@ShippingState", parameters.ShippingState },
                { "@ShippingZip", parameters.ShippingZip },
                { "@ShippingAddress", parameters.ShippingAddress },
                { "@ShippingAddress2", parameters.ShippingAddress2 }
            };

            //UPDATE table_name SET column1 = value, column2 = value2,... WHERE some_column = some_value
            string query = "UPDATE Address SET BillingCity=@BillingCity, BillingState=@BillingState, BillingZip=@BillingZip, BillingAddress=@BillingAddress, BillingAddress2=@BillingAddress2, " +
                                              "ShippingCity=@ShippingCity, ShippingState=@ShippingState, ShippingZip=@ShippingZip, ShippingAddress=@ShippingAddress, ShippingAddress2=@ShippingAddress2 " +
                                              "WHERE CustomerUUID=@CustomerUUID;";

            int result = dataAccess.Update(query, param);

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

        public NonQueryResultModel DeleteRecord(DeleteAddressModel parameters)
        {
            throw new NotImplementedException();
        }
    }
}