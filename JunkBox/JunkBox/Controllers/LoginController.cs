using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JunkBox.Controllers {
    public class LoginController : Controller {
        // GET: Login
        public String Index () {
            return "Fuggg";
        }

        // GET: Login/Details/5
        public String Login (String id) {
            id = id.Replace("PERIODHERE", ".");
            String password = id.Split(',')[1];
            //return password;
            return HttpUtility.UrlDecode(id).ToString();
        }

        // GET: Login/Create
        public ActionResult Create () {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create (FormCollection collection) {
            try {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit (int id) {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit (int id, FormCollection collection) {
            try {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete (int id) {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete (int id, FormCollection collection) {
            try {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
    }
}
