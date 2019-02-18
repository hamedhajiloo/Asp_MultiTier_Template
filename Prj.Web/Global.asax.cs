using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Prj.IoC;
using System.Data.Entity;
using Prj.DataAccess.Context;
using Prj.DataAccess.Migrations;
using System.Web.Optimization;

namespace Prj.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetDbInitializer();
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = SmObjectFactory.Container.GetInstance<Microsoft.AspNet.SignalR.IDependencyResolver>();
           // var captchaManager = (DefaultCaptchaManager)CaptchaUtils.CaptchaManager;

        }


        private static void SetDbInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
            SmObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "GET" && !Request.IsLocal)
            {
                string url = Request.Url.AbsoluteUri;

                if (!url.StartsWith("https://www.") &&
                    !url.StartsWith("http://test.")) // برای به مشکل بر نخوردن پروژه تست
                {
                    var newUrl = url.Replace("http:", "https:");

                    if (!newUrl.Contains("www"))
                        newUrl = newUrl.Replace("https://", "https://www.");

                    Response.Redirect(newUrl);
                }
            }
        }
    }
    public class StructureMapControllerFactory:DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, $"Resource not found : {requestContext.HttpContext.Request.Path}");
            }
            return SmObjectFactory.Container.GetInstance(controllerType) as Controller;

        }
    }
}