using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JunkBox
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Custom Route, Here is where we can make new or special API functions, or calls
            //Or routes, or however the hell you want to generalize what this does.
            config.Routes.MapHttpRoute(
                name: "searchName",
                routeTemplate: "api/{controller}/{firstName}/{lastName}",
                defaults: new { lastName = RouteParameter.Optional }
            );
        }
    }
}
