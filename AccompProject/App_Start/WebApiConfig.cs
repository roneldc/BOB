using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace AccompProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));

            config.Routes.MapHttpRoute(
                     "OfficeApi",
                     "api/{controller}/{action}/{mnt}/{yr}"
           );
            config.Routes.MapHttpRoute(
                   "MobileAPI",
                   "api/{controller}/{action}/{username}/{password}"
         );

            config.Routes.MapHttpRoute(
                    "OfficeApi1",
                    "api/{controller}/{action}/{myid}"
          );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
