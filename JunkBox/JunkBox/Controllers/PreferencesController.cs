using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JunkBox.Models;
using JunkBox.DataAccess;

namespace JunkBox.Controllers
{
    public class PreferencesController : Controller
    {
        private IDataAccess dataAccess = MySqlDataAccess.GetDataAccess();

        //POST: Prefrences/ValidateAddress/{data}
        [HttpPost]
        public ActionResult UpdateAddress(PrefrenceAddressModel data)
        {

            System.Windows.Forms.MessageBox.Show(data.email);

            return Json(new { result = "Success" });
        }
    }
}