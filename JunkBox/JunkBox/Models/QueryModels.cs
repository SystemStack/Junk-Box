using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JunkBox.Models
{
    public class QueryGetSettingsModel
    {
        public string email { get; set; }
    }

    public class QuerySetSettingsModel
    {
        public string email { get; set; }
        public string category { get; set; }
        public string categoryId { get; set; }
        public string price { get; set; }
        public QueryFrequencyOptions frequencyOptions { get; set; }
    }

    public class QueryFrequencyOptions
    {
        public string label { get; set; }
        public int value { get; set; }
    }

}