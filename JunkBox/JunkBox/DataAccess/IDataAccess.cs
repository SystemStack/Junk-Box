using System.Collections.Generic;
using System.Data.Common;

namespace JunkBox.DataAccess {
    //This will probably end up being updated further with different functions like "Select, update, insert, delete, etc"
    interface IDataAccess
    {
       
        List<IDictionary<string, object>> Select(string query, IDictionary<string, object> whereParameters);
        int Insert(string query, IDictionary<string, object> parameters);
        int Delete(string table, string key, string value);
        int Update(string query, IDictionary<string, object> parameters);
    }
}
