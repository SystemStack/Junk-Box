using System.Collections.Generic;

namespace JunkBox.DataAccess
{
    public interface IDataAccess
    {
        List<IDictionary<string, object>> Select(string query, IDictionary<string, object> parameters);
        int Insert(string query, IDictionary<string, object> parameters);
        int Delete(string query, IDictionary<string, object> parameters);
        int Update(string query, IDictionary<string, object> parameters);
    }
}
