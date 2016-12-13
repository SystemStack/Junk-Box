using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using JunkBox.DataAccess;
using JunkBox.Models;
using System.Collections.Generic;

namespace JunkBox.Tests
{
    [TestClass]
    public class QueryTableTest
    {
        [TestMethod]
        public void SelectQueryRecord()
        {
            QueryTable table = QueryTable.Instance(new QueryDataAccess());

            QueryResultModel expectedResult = new QueryResultModel() {
                QueryID = 2,
                CustomerUUID = "CustUUID02",
                Frequency = "Freq02",
                PriceLimit = "PriceLimit02",
                Category = "Cat02",
                CategoryID = "CatID02"
            };

            QueryResultModel result = table.SelectRecord(new SelectQueryModel() { CustomerUUID = "CustUUID02"});

            Assert.AreEqual(expectedResult.QueryID, result.QueryID);
            Assert.AreEqual(expectedResult.CustomerUUID, result.CustomerUUID);
            Assert.AreEqual(expectedResult.Frequency, result.Frequency);
            Assert.AreEqual(expectedResult.PriceLimit, result.PriceLimit);
            Assert.AreEqual(expectedResult.Category, result.Category);
            Assert.AreEqual(expectedResult.CategoryID, result.CategoryID);
        }

        [TestMethod]
        public void SelectQueryRecordNonExistent()
        {
            QueryTable table = QueryTable.Instance(new QueryDataAccess());

            QueryResultModel expectedResult = new QueryResultModel();

            QueryResultModel result = table.SelectRecord(new SelectQueryModel() { CustomerUUID = "NonExistentID" });

            Assert.AreEqual(expectedResult.QueryID, result.QueryID);
            Assert.AreEqual(expectedResult.CustomerUUID, result.CustomerUUID);
            Assert.AreEqual(expectedResult.Frequency, result.Frequency);
            Assert.AreEqual(expectedResult.PriceLimit, result.PriceLimit);
            Assert.AreEqual(expectedResult.Category, result.Category);
            Assert.AreEqual(expectedResult.CategoryID, result.CategoryID);
        }

        [TestMethod]
        public void DeleteQueryRecord()
        {
            QueryTable table = QueryTable.Instance(new QueryDataAccess());

            NonQueryResultModel expectedResult = new NonQueryResultModel() {
                Success = true
            };

            NonQueryResultModel result = table.DeleteRecord(new DeleteQueryModel() { CustomerUUID = "CustUUID03" });

            Assert.AreEqual(expectedResult.Success, result.Success);
        }

        [TestMethod]
        public void DeleteQueryRecordNonExistent()
        {
            QueryTable table = QueryTable.Instance(new QueryDataAccess());

            NonQueryResultModel expectedResult = new NonQueryResultModel()
            {
                Success = false
            };

            NonQueryResultModel result = table.DeleteRecord(new DeleteQueryModel() { CustomerUUID = "NonExistentID" });

            Assert.AreEqual(expectedResult.Success, result.Success);
        }

        [TestMethod]
        public void UpdateQueryRecord()
        {
            QueryTable table = QueryTable.Instance(new QueryDataAccess());

            NonQueryResultModel expectedResult = new NonQueryResultModel()
            {
                Success = true
            };

            NonQueryResultModel result = table.UpdateRecord(new UpdateQueryModel() { CustomerUUID = "CustUUID01" });

            Assert.AreEqual(expectedResult.Success, result.Success);
        }

        [TestMethod]
        public void UpdateQueryRecordNonExistent()
        {
            QueryTable table = QueryTable.Instance(new QueryDataAccess());

            NonQueryResultModel expectedResult = new NonQueryResultModel()
            {
                Success = false
            };

            NonQueryResultModel result = table.UpdateRecord(new UpdateQueryModel() { CustomerUUID = "NonExistentID" });

            Assert.AreEqual(expectedResult.Success, result.Success);
        }

        [TestMethod]
        public void InsertQueryRecord()
        {
            QueryTable table = QueryTable.Instance(new QueryDataAccess());

            NonQueryResultModel expectedResult = new NonQueryResultModel()
            {
                Success = true
            };

            NonQueryResultModel result = table.InsertRecord(new InsertQueryModel() { CustomerUUID = "CustUUID04" });

            Assert.AreEqual(expectedResult.Success, result.Success);
        }

        [TestMethod]
        public void InsertQueryRecordExistent()
        {
            QueryTable table = QueryTable.Instance(new QueryDataAccess());

            NonQueryResultModel expectedResult = new NonQueryResultModel()
            {
                Success = false
            };

            NonQueryResultModel result = table.InsertRecord(new InsertQueryModel() { CustomerUUID = "CustUUID01" });

            Assert.AreEqual(expectedResult.Success, result.Success);
        }
    }

    public class QueryDataAccess : IDataAccess
    {
        public int Delete(string query, IDictionary<string, object> parameters)
        {
            var delete =
                from entry in queryTable
                where entry["CustomerUUID"] == parameters["@CustomerUUID"]
                select entry;

            return delete.Count();
        }

        public int Insert(string query, IDictionary<string, object> parameters)
        {
            var insert =
                from entry in queryTable
                where entry["CustomerUUID"] == parameters["@CustomerUUID"]
                select entry;

            if(insert.Count() == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<IDictionary<string, object>> Select(string query, IDictionary<string, object> parameters)
        {
            var results = 
                from entry in queryTable
                where entry["CustomerUUID"] == parameters["@CustomerUUID"]
                select entry;

            List<IDictionary<string, object>> payload = new List<IDictionary<string, object>>();

            foreach(IDictionary<string, object> entry in results)
            {
                payload.Add(entry);
            }

            return payload;
        }

        public int Update(string query, IDictionary<string, object> parameters)
        {
            var update =
                from entry in queryTable
                where entry["CustomerUUID"] == parameters["@CustomerUUID"]
                select entry;

            return update.Count();
        }

        private List<IDictionary<string, object>> queryTable = new List<IDictionary<string, object>>() {
            new Dictionary<string, object>() {
                { "QueryID", 1 },
                { "CustomerUUID", "CustUUID01" },
                { "Frequency", "Freq01" },
                { "PriceLimit", "PriceLimit01" },
                { "Category", "Cat01" },
                { "CategoryID", "CatID01" }
            },
            new Dictionary<string, object>() {
                { "QueryID", 2 },
                { "CustomerUUID", "CustUUID02" },
                { "Frequency", "Freq02" },
                { "PriceLimit", "PriceLimit02" },
                { "Category", "Cat02" },
                { "CategoryID", "CatID02" }
            },
            new Dictionary<string, object>() {
                { "QueryID", 3 },
                { "CustomerUUID", "CustUUID03" },
                { "Frequency", "Freq03" },
                { "PriceLimit", "PriceLimit03" },
                { "Category", "Cat03" },
                { "CategoryID", "CatID03" }
            }
        };
    }
}
