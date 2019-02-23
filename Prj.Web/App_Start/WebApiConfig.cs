using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Prj.IoC;
using Prj.Web.Attributes;
using Sepehran.Bmeh.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Prj.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Formatters.JsonFormatter.SupportedMediaTypes
              .Add(new MediaTypeHeaderValue("application/json"));

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/x-www-form-urlencoded"));

            var container = SmObjectFactory.Container;
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), new SmHttpControllerActivator(container));

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new LogUsedServiceFilterAttribute());
            config.Filters.Add(new HttpErrorHandlerFilterAttribute());




            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            //Change null string to "-"
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { ContractResolver = new NullToEmptyStringResolver() };

        }
    }
}
