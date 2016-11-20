using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using JunkBox.DataAccess;
using System.Security.Cryptography;

namespace JunkBox.Controllers {
    public class HomeController : Controller {

        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        // POST: Login/Login/email@site.domain,passW0rd1
        [HttpPost]
        public String GetRecentPurchases (String id) {
            id = id.Replace("PERIODHERE", ".");

            List<Dictionary<string, string>> results = dataAccess.Select("SELECT * FROM Customer WHERE Email = '" + id + "'");

            foreach (Dictionary<string, string> row in results) {
                string keys = "";
                string values = "";
                foreach (KeyValuePair<string, string> item in row) {
                    keys += " " + item.Key;
                    values += " " + item.Value;
                }
                System.Windows.Forms.MessageBox.Show("KEYS: " + keys + " VALUES: " + values);
                System.Windows.Forms.MessageBox.Show("FirstName: " + row["FirstName"].ToString());
            }
            dataAccess.CloseConnection();
            return HttpUtility.UrlDecode(id).ToString();
        }
    }
}