using System.Collections.Generic;
using System.Data.Common;

namespace JunkBox.DataAccess {
    //This will probably end up being updated further with different functions like "Select, update, insert, delete, etc"
    interface IDataAccess
    {
       
        List<IDictionary<string, object>> Select(string query, IDictionary<string, object> parameters);
        int Insert(string query, IDictionary<string, object> parameters);
        int Delete(string query, IDictionary<string, object> parameters);
        int Update(string query, IDictionary<string, object> parameters);
    }
}
