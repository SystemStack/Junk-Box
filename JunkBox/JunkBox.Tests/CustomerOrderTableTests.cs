using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JunkBox.DataAccess;
using JunkBox.Models;
using System.Collections.Generic;

namespace JunkBox.Tests
{
    [TestClass]
    public class CustomerOrderTableTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    public class CustomerOrderDataAccess : IDataAccess
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

        private List<Dictionary<string, object>> customerOrderTable = new List<Dictionary<string, object>>() {
            new Dictionary<string, object>() {
                { "OrderID", "OrderID01" },
                { "CustomerUUID", "CustUUID01" },
                { "PurchasePrice", "PurPrice01" },
                { "Title", "Title01" },
                { "CheckoutSessionID", "CheckSessID01" },
                { "ExpirationDate", "ExpDate01" },
                { "ImageURL", "ImgURL01" },
                { "TimeStamp", "TimeStamp01" }
            },
            new Dictionary<string, object>() {
                { "OrderID", "OrderID02" },
                { "CustomerUUID", "CustUUID02" },
                { "PurchasePrice", "PurPrice02" },
                { "Title", "Title02" },
                { "CheckoutSessionID", "CheckSessID02" },
                { "ExpirationDate", "ExpDate02" },
                { "ImageURL", "ImgURL02" },
                { "TimeStamp", "TimeStamp02" }
            },
            new Dictionary<string, object>() {
                { "OrderID", "OrderID03" },
                { "CustomerUUID", "CustUUID03" },
                { "PurchasePrice", "PurPrice03" },
                { "Title", "Title03" },
                { "CheckoutSessionID", "CheckSessID03" },
                { "ExpirationDate", "ExpDate03" },
                { "ImageURL", "ImgURL03" },
                { "TimeStamp", "TimeStamp03" }
            },
        };
    }
}
