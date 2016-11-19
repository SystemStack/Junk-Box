using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace JunkBox.DataAccess
{
    //This will probably end up being updated further with different functions like "Select, update, insert, delete, etc"
    interface IDataAccess
    {
        DbDataReader query(string query);
        void OpenConnection();
        void CloseConnection();

        DbDataReader select(string query);
        int insert(string query);
    }
}
