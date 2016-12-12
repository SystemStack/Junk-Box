using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using JunkBox.DataAccess;
using JunkBox.Models;
using System.Collections.Generic;

namespace JunkBox.Tests
{
    [TestClass]
    public class QueryTableTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataAccess mockData = new DataAccess();

            QueryTable table = QueryTable.Instance(mockData);

            QueryResultModel expectedResult = new QueryResultModel() {
                QueryID = 1,
                CustomerUUID = "1234-5678",
                Frequency = "DAILY",
                PriceLimit = "5",
                Category = "Stuff",
                CategoryID = "50"
            };

            QueryResultModel result = table.SelectRecord(new SelectQueryModel());

            Assert.AreEqual(1, result.QueryID);
            Assert.AreEqual("1234-5678", result.CustomerUUID);
            Assert.AreEqual("DAILY", result.Frequency);
            Assert.AreEqual("5", result.PriceLimit);
            Assert.AreEqual("50", result.CategoryID);
        }
    }

    public class DataAccess : IDataAccess
    {
        public int Delete(string query, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public int Insert(string query, IDictionary<string, object> parameters)
        {
            return 1;
        }

        public List<IDictionary<string, object>> Select(string query, IDictionary<string, object> parameters)
        {
            List<IDictionary<string, object>> results = new List<IDictionary<string, object>>();
            IDictionary<string, object> value = new Dictionary<string, object>();
            value.Add("QueryID", 1);
            value.Add("CustomerUUID", "1234-5678");
            value.Add("Frequency", "DAILY");
            value.Add("PriceLimit", "5");
            value.Add("Category", "Stuff");
            value.Add("CategoryID", "50");

            results.Add(value);

            return results;
        }

        public int Update(string query, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
