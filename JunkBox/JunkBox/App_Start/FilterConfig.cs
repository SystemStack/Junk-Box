﻿using System.Web.Mvc;

namespace JunkBox {
    public class FilterConfig {
        public static void RegisterGlobalFilters (GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
