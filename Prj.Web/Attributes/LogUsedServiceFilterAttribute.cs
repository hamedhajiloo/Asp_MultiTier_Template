using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Prj.DataAccess.Context;
using Prj.IoC;
using Prj.Services.Utilities;

namespace Prj.Web.Attributes
{
    public static class LastParametersModel
    {
        public static string LastParameters { get; set; }
    }

    public class LogUsedServiceFilterAttribute : ActionFilterAttribute
    {
        private readonly Func<IUnitOfWork> _unitOfWork =
            SmObjectFactory.Container.GetInstance<IUnitOfWork>;

        private readonly Func<IWebMethodService> _usedWebMethodService =
            SmObjectFactory.Container.GetInstance<IWebMethodService>;

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            string controllerName = actionContext.ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = actionContext.ControllerContext.RouteData.Values["action"].ToString();
            LastParametersModel.LastParameters = new System.IO.StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd();

            var usedWebMethod = await _usedWebMethodService().FindAsync(controllerName, actionName);

            if (usedWebMethod == null)
                _usedWebMethodService().Add(controllerName, actionName, 1, LastParametersModel.LastParameters.Length <= 1000 ? LastParametersModel.LastParameters : "Large data");
            else
            {
                usedWebMethod.CalledCount++;
                usedWebMethod.LastParameters = LastParametersModel.LastParameters.Length <= 1000 ? LastParametersModel.LastParameters : "Large data";
            }

            await _unitOfWork().SaveChangesAsync();
        }
    }
}