using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using VMS.Models;

namespace VMS.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional,namespaceName= "VMS.Api" }
               
             );

            config.Routes.MapHttpRoute(
                name: "ESIApi",
                routeTemplate: "esi-api/{controller}/{action}",
                defaults:new { namespaceName= "VMS.ESIApi"}
                );
            config.Filters.Add(new WebApiExceptionFilterAttribute());
        }
    }
}