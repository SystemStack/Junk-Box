using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JunkBox.DataAccess;
using System.Collections.Generic;

namespace JunkBox.Tests
{
    [TestClass]
    public class CustomerTableTests
    {
        [TestMethod]
        public void CustomerTableTest1()
        {
        }
    }

    public class CustomerDataAccess : IDataAccess
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

        private List<Dictionary<string, object>> customerTable = new List<Dictionary<string, object>>() {
            new Dictionary<string, object>() {
                { "CustomerID", 1 },
                { "CustomerUUID", "CustUUID01" },
                { "FirstName", "First01" },
                { "LastName", "Last01" },
                { "Phone", "Phone01" },
                { "Hash", "Hash01" },
                { "Salt", "Salt01" },
                { "Email", "Email01" }
            },
            new Dictionary<string, object>() {
                { "CustomerID", 2 },
                { "CustomerUUID", "CustUUID02" },
                { "FirstName", "First02" },
                { "LastName", "Last02" },
                { "Phone", "Phone02" },
                { "Hash", "Hash02" },
                { "Salt", "Salt02" },
                { "Email", "Email02" }
            },
            new Dictionary<string, object>() {
                { "CustomerID", 3 },
                { "CustomerUUID", "CustUUID03" },
                { "FirstName", "First03" },
                { "LastName", "Last03" },
                { "Phone", "Phone03" },
                { "Hash", "Hash03" },
                { "Salt", "Salt03" },
                { "Email", "Email03" }
            }
        };
    }
}
