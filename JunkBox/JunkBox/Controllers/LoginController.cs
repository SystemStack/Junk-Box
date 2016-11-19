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
            //System.Windows.Forms.MessageBox.Show("HEY!!!!!");

            dataAccess.OpenConnection();

            DbDataReader reader = dataAccess.select("SELECT * FROM Customer");
            while(reader.Read())
            {
                
                System.Windows.Forms.MessageBox.Show((string)reader["FirstName"]);
            }

            dataAccess.CloseConnection();

            return HttpUtility.UrlDecode(id).ToString();
        }

        // POST: Login/Login/email@site.domain,passW0rd1
        [HttpPost]
        public String Register (String id) {
            id = id.Replace("PERIODHERE", ".");
            String password = id.Split(',')[1];
            //return password;
            return HttpUtility.UrlDecode(id).ToString();
        }

    }
}
