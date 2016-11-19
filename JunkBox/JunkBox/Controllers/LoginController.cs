using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JunkBox.DataAccess;
using System.Data.Common;

namespace JunkBox.Controllers {
    public class LoginController : Controller {

        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        // POST: Login/Login/email@site.domain,passW0rd1
        [HttpPost]
        public String Login (String id) {
            id = id.Replace("PERIODHERE", ".");
            String password = id.Split(',')[1];
            //return password;
            System.Windows.Forms.MessageBox.Show(id);

            dataAccess.OpenConnection();

            DbDataReader reader = dataAccess.select("SELECT * FROM Customer");
            while(reader.Read())
            {
                
                //System.Windows.Forms.MessageBox.Show((string)reader["FirstName"]);
            }

            dataAccess.CloseConnection();

            return HttpUtility.UrlDecode(id).ToString();
        }

        // POST: Login/Login/email@site.domain,passW0rd1
        [HttpPost]
        public String Register (String id) {
            //System.Windows.Forms.MessageBox.Show(id.GetType().ToString());


            dataAccess.OpenConnection();

            dataAccess.insert("INSERT INTO `cs341_t5`.`Customer` (`CustomerID`, `QueryID`, `AddressID`, `FirstName`, `LastName`, `Phone`, `Hash`, `Salt`, `Email`) VALUES (NULL, '3', '3', 'REGISTER_TEST', 'Test', '1112223333', '4', 'r', 'walter@test.com');");

            dataAccess.CloseConnection();

            /*
            id = id.Replace("PERIODHERE", ".");
            String password = id.Split(',')[1];
            //return password;
            */
            return HttpUtility.UrlDecode(id).ToString();
        }

    }
}
