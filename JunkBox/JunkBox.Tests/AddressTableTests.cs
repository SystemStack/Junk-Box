using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JunkBox.DataAccess;
using JunkBox.Models;
using System.Collections.Generic;

namespace JunkBox.Tests
{
    [TestClass]
    public class AddressTableTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    public class AddressDataAccess : IDataAccess
    {
        public int Delete(string query, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public int Insert(string query, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public List<IDictionary<string, object>> Select(string query, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public int Update(string query, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        private List<Dictionary<string, object>> addressTable = new List<Dictionary<string, object>>() {
            new Dictionary<string, object>() {
                { "AddressID", 1 },
                { "CustomerUUID", "CustUUID01" },
                { "BillingCity", "BillCity01" },
                { "BillingState", "BillState01" },
                { "BillingZip", "BillZip01" },
                { "BillingAddress", "BillAddr01" },
                { "BillingAddress2", "BillAddr201" },
                { "ShippingCity", "ShipCity01" },
                { "ShippingState", "ShipState01" },
                { "ShippingZip", "ShipZip01" },
                { "ShippingAddress", "ShipAddr01" },
                { "ShippingAddress2", "ShipAddr201" },
            },
            new Dictionary<string, object>() {
                { "AddressID", 2 },
                { "CustomerUUID", "CustUUID02" },
                { "BillingCity", "BillCity02" },
                { "BillingState", "BillState02" },
                { "BillingZip", "BillZip02" },
                { "BillingAddress", "BillAddr02" },
                { "BillingAddress2", "BillAddr202" },
                { "ShippingCity", "ShipCity02" },
                { "ShippingState", "ShipState02" },
                { "ShippingZip", "ShipZip02" },
                { "ShippingAddress", "ShipAddr02" },
                { "ShippingAddress2", "ShipAddr202" },
            },
            new Dictionary<string, object>() {
                { "AddressID", 3 },
                { "CustomerUUID", "CustUUID03" },
                { "BillingCity", "BillCity03" },
                { "BillingState", "BillState03" },
                { "BillingZip", "BillZip03" },
                { "BillingAddress", "BillAddr03" },
                { "BillingAddress2", "BillAddr203" },
                { "ShippingCity", "ShipCity03" },
                { "ShippingState", "ShipState03" },
                { "ShippingZip", "ShipZip03" },
                { "ShippingAddress", "ShipAddr03" },
                { "ShippingAddress2", "ShipAddr203" },
            },
        };
    }
}
