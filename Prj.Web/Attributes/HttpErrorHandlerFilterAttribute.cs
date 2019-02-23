using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Prj.DataAccess.Context;
using Prj.Services.Utilities;
using Prj.Domain.Supports;
using Prj.IoC;
using Microsoft.AspNet.Identity;

namespace Prj.Web.Attributes
{
    public class HttpErrorHandlerFilterAttribute : ExceptionFilterAttribute
    {
        private readonly Func<IUnitOfWork> _unitOfWork =
            SmObjectFactory.Container.GetInstance<IUnitOfWork>;
        private readonly Func<IErrorLogService> _errorLogService =
            SmObjectFactory.Container.GetInstance<IErrorLogService>;

        
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Log(actionExecutedContext);
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// ذخیره سازی اطلاعات ارور های رخ داد
        /// </summary>
        /// <param name="e"></param>
        private void Log(HttpActionExecutedContext e)
        {
            var errorLog = new ErrorLog()
            {
                Exception = e.Exception.Message,
                Route = e.Request.RequestUri.ToString(),
                InsertDate = DateTime.Now,
                UserIPAddress = HttpContext.Current.Request.UserHostAddress,
                Device = HttpContext.Current.Request.UserAgent,
                UserId = HttpContext.Current.User.Identity.GetUserId(),
                Parameters =  LastParametersModel.LastParameters.Length <= 1000 ? LastParametersModel.LastParameters : "Large data"
            };

            //تعیین خطای داخلی

            var ex = e.Exception.InnerException;

            while (ex != null)
            {
                ex = ex.InnerException;
                if (ex != null && ex.InnerException == null)
                    errorLog.InnerException = ex.Message;
            }

            _unitOfWork().RejectChanges();
            _errorLogService().Add(errorLog);
            _unitOfWork().SaveChanges();
        }
    }
}