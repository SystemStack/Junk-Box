using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JunkBox.Models
{
    public class RegisterModel
    {
        public string email { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        //public string password { get; set; }
        //public string password2 { get; set; }
        public string phone { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string hash { get; set; }
        public string salt { get; set; }
    }
}