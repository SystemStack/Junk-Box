using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace JunkBox.Controllers {
    public class LoginController : ApiController {
        [HttpPost]
        public String login(String User) {
            return User;
        }
    }
}
