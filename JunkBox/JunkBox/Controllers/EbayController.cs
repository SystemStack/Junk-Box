using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using JunkBox.Models;
using JunkBox.Common;

namespace JunkBox.Controllers
{
    public class EbayController : Controller
    {

        //POST: Ebay/GetSomething/{data}
        [HttpPost]
        public ActionResult GetSomething(EbayGetSomethingModel data)
        {
            return Json(new { result=Ebay.GetTimestamp() });
        }
    }
}