using System.Collections.Generic;
using System.Data.Common;

namespace JunkBox.DataAccess {
    //This will probably end up being updated further with different functions like "Select, update, insert, delete, etc"
    interface IDataAccess
    {
        DbDataReader Query(string query);
        void OpenConnection();
        void CloseConnection();

        List<Dictionary<string, string>> Select(string query);
        int Insert(string table, Dictionary<string, string> parameters);
        int Delete(string table, string key, string value);
        int Update(string table, Dictionary<string, string> items, string key, string value);
    }
}
