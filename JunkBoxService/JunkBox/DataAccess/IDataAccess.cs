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

        DbDataReader findUserPurchasePrice(String userName);
        DbDataReader findCustomer(String userName);

      
        void updateCustomerName(String firstName, string lastName, String userName);
        void updateCustomerEmail(String firstName, String lastName, String oldEmail, String newEmail);
        void updateCustomerPhoneNumber(String firstName, String lastName, String email, String newPhone);
    }
}
