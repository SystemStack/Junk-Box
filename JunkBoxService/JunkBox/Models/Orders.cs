using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JunkBox.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        public double PurchasePrice { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}