using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JunkBox.Models
{
    public class PreferenceAddressModel
    {
        public string email { get; set; }
        public string streetName { get; set; }
        public string streetName2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
    }

    public class PreferenceChangePasswordModel
    {
        public string email { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class PreferenceHaltPurchaseModel
    {
        public bool action { get; set; }
    }

    public class PreferenceGetAddressModel
    {
        public string email { get; set; }
    }
}