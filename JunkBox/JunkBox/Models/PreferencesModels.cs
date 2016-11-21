using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JunkBox.Models
{
    public class PrefrenceAddressModel
    {
        public string email { get; set; }
        public string streetName { get; set; }
        public string streetName2 { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
    }
}