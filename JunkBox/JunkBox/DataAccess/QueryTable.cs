using System;
using System.Collections.Generic;
using System.Linq;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public class QueryTable : DataTable, IDataTable<QueryResultModel, SelectQueryModel, NonQueryResultModel, InsertQueryModel, NonQueryResultModel, UpdateQueryModel, NonQueryResultModel, DeleteQueryModel>
    {
        private static QueryTable instance = null;

        public static QueryTable Instance()
        {
            if (instance == null)
            {
                instance = new QueryTable();
            }

            return instance;
        }

        public QueryResultModel SelectRecord(SelectQueryModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID }
            };
            string query = "SELECT * FROM Query WHERE CustomerUUID=@CustomerUUID";

            IDictionary<string, object> queryResult = dataAccess.Select(query, param).First();

            QueryResultModel payload = new QueryResultModel()
            {
                QueryID = (int)queryResult["QueryID"],
                CustomerUUID = (string)queryResult["CustomerUUID"],
                Frequency = (string)queryResult["Frequency"],
                PriceLimit = (string)queryResult["PriceLimit"],
                Category = (string)queryResult["Category"],
                CategoryID = (string)queryResult["CategoryID"]
            };

            return payload;
        }

        public NonQueryResultModel InsertRecord(InsertQueryModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID },
                { "@Frequency", parameters.Frequency },
                { "@PriceLimit", parameters.PriceLimit },
                { "@Category", parameters.Category },
                { "@CategoryID", parameters.CategoryID }
            };

            string query = "INSERT INTO Query (CustomerUUID, Frequency, PriceLimit, Category, CategoryID)" +
                                       " VALUES (@CustomerUUID, @Frequency, @PriceLimit, @Category, @CategoryID);";

            int result = dataAccess.Insert(query, param);

            return PrepareNonQueryResult(result);
        }

        public NonQueryResultModel UpdateRecord(UpdateQueryModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID },
                { "@Category", parameters.Category },
                { "@CategoryID", parameters.CategoryID },
                { "@Frequency", parameters.Frequency },
                { "@PriceLimit", parameters.PriceLimit }
            };

            string query = "UPDATE Query SET Category=@Category, CategoryID=@CategoryID, Frequency=@Frequency, PriceLimit=@PriceLimit WHERE CustomerUUID=@CustomerUUID";

            int result = dataAccess.Update(query, param);

            return PrepareNonQueryResult(result);
        }

        public NonQueryResultModel DeleteRecord(DeleteQueryModel parameters)
        {
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
                { "@CustomerUUID", parameters.CustomerUUID }
            };

            string query = "DELETE FROM Query WHERE CustomerUUID=@CustomerUUID;";

            int result = dataAccess.Delete(query, param);

            return PrepareNonQueryResult(result);
        }

        public List<QueryResultModel> SelectAll(SelectQueryModel parameters)
        {
            throw new NotImplementedException();
        }
    }
}