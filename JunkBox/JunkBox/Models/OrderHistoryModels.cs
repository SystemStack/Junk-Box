using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JunkBox.Models
{
    public class OrderHistoryCustomerDataModel
    {
        public string email { get; set; }
    }

    public class OrderHistoryGetGuestCheckoutSessionModel
    {
        public string checkoutSessionId { get; set; }
    }
}