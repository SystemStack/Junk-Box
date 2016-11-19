using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JunkBox.Controllers {
    public class LoginController : Controller {

        // POST: Login/Login/email@site.domain,passW0rd1
        [HttpPost]
        public String Login (String id) {
            id = id.Replace("PERIODHERE", ".");
            String password = id.Split(',')[1];
            //return password;
            System.Windows.Forms.MessageBox.Show("HEY!!!!!");
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
