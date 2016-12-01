using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class QueryTable : IDataTable<QueryDataModel, CustomerUUIDModel, NonQueryResultModel, InsertQueryModel, NonQueryResultModel, CustomerUUIDModel, NonQueryResultModel, CustomerUUIDModel>
    {
        private static IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        private static QueryTable instance = null;

        private QueryTable()
        {
        }

        public static QueryTable Instance()
        {
            if (instance == null)
            {
                instance = new QueryTable();
            }

            return instance;
        }

        public NonQueryResultModel InsertQuery(InsertQueryModel queryData)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", queryData.CustomerUUID },
                { "@Frequency", queryData.Frequency },
                { "@PriceLimit", queryData.PriceLimit },
                { "@Category", queryData.Category },
                { "@CategoryID", queryData.CategoryID }
            };
            //INSERT INTO table_name (column1,column2,column3,...) VALUES (value1, value2, value3,...);
            string query = "INSERT INTO Query (CustomerUUID, Frequency, PriceLimit, Category, CategoryID)" +
                                       " VALUES (@CustomerUUID, @Frequency, @PriceLimit, @Category, @CategoryID);";

            int result = dataAccess.Insert(query, parameters);

            bool succeeded = false;
            if (result == 1)
            {
                succeeded = true;
            }

            NonQueryResultModel payload = new NonQueryResultModel() {
                Success = succeeded
            };

            return payload;
        }

        public QueryDataModel GetQueryData(CustomerUUIDModel customerUuid)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", customerUuid.CustomerUUID }
            };
            string query = "SELECT * FROM Query WHERE CustomerUUID=@CustomerUUID";

            IDictionary<string, object> queryResult = dataAccess.Select(query, parameters).First();

            QueryDataModel payload = new QueryDataModel() {
                Frequency = (string)queryResult["Frequency"],
                PriceLimit = (string)queryResult["PriceLimit"],
                Category = (string)queryResult["Category"],
                CategoryID = (string)queryResult["CategoryID"]
            };

            return payload;
        }

        public NonQueryResultModel UpdateQueryData(QueryDataModel queryData, CustomerUUIDModel customerUuid)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@CustomerUUID", customerUuid.CustomerUUID },
                { "@Category", queryData.Category },
                { "@CategoryID", queryData.CategoryID },
                { "@Frequency", queryData.Frequency },
                { "@PriceLimit", queryData.PriceLimit }
            };

            //UPDATE table_name SET column1 = value, column2 = value2,... WHERE some_column = some_value
            string query = "UPDATE Query SET Category=@Category, CategoryID=@CategoryID, Frequency=@Frequency, PriceLimit=@PriceLimit WHERE CustomerUUID=@CustomerUUID";

            int result = dataAccess.Update(query, parameters);

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


        public QueryDataModel SelectRecord(CustomerUUIDModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID }
            };
            string query = "SELECT * FROM Query WHERE CustomerUUID=@CustomerUUID";

            IDictionary<string, object> queryResult = dataAccess.Select(query, param).First();

            QueryDataModel payload = new QueryDataModel()
            {
                Frequency = (string)queryResult["Frequency"],
                PriceLimit = (string)queryResult["PriceLimit"],
                Category = (string)queryResult["Category"],
                CategoryID = (string)queryResult["CategoryID"]
            };

            return payload;
        }

        public NonQueryResultModel InsertRecord(InsertQueryModel parameters)
        {
            throw new NotImplementedException();
        }

        public NonQueryResultModel UpdateRecord(CustomerUUIDModel parameters)
        {
            throw new NotImplementedException();
        }

        public NonQueryResultModel DeleteRecord(CustomerUUIDModel parameters)
        {
            throw new NotImplementedException();
        }
    }
}