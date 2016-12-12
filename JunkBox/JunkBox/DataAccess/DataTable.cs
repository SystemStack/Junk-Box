using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JunkBox.Models;

namespace JunkBox.DataAccess
{
    public abstract class DataTable
    {
        protected static IDataAccess dataAccess;// = MySqlDataAccess.GetDataAccess();

        protected DataTable(IDataAccess access)
        {
            dataAccess = access;
        }

        protected NonQueryResultModel PrepareNonQueryResult(int result, int expectedResult = 1)
        {
            bool succeeded = false;
            if (result == expectedResult)
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