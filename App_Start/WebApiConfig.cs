using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ShottBowing
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
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           // EnableCorsAttribute cors = new EnableCorsAttribute("*","*","*");
           // config.EnableCors(cors);

            config.EnableCors();

           config.Formatters.Remove(config.Formatters.XmlFormatter);
          //  config.Filters.Add(new BasicAuthenticationAttribute());
          //  config.Formatters.Remove(config.Formatters.JsonFormatter);//XmlFormatter
           // config.Formatters.Remove(config.Formatters.JsonFormatter);

            //  config.Formatters.JsonFormatter.SerializerSettings.Formatting =
            //                 Newtonsoft.Json.Formatting.Indented;
            //  config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //      new CamelCasePropertyNamesContractResolver();
        }
    }
}
