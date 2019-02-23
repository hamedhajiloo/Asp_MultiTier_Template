using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Web;
using Prj.Services.Users;
using Prj.IoC;
using Prj.Services.Utilities;
using Prj.Services.Extensions;
using Prj.Services.Models;

namespace HmFadak.ArPanel.Web.Attributes
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {

        private readonly Func<IUserTokenService> _userTokenService =
            SmObjectFactory.Container.GetInstance<IUserTokenService>;

        private readonly Func<IXmlService> _xmlService =
            SmObjectFactory.Container.GetInstance<IXmlService>;

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.IsLocal)
                return;

            if (SkipAuthorization(actionContext))
            {
                return;
            }

            if (actionContext.Request.Headers.Authorization == null)
            {
                // if authorization in header is empty
                actionContext.Response = actionContext.Request
                     .CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            var accessToken = actionContext.Request.Headers.Authorization.Parameter;
            if (string.IsNullOrWhiteSpace(accessToken) ||
                accessToken.Equals("undefined", StringComparison.OrdinalIgnoreCase))
            {
                actionContext.Response = actionContext.Request
                     .CreateResponse(HttpStatusCode.Unauthorized,
                     new { error = JwtStatusResult.InvalidToken });
                return;
            }

            if (HttpContext.Current.User.Identity.GetUserId() == null ||
                !HttpContext.Current.User.Identity.GetClaimIdentity().Claims.Any())
            {
                // this is not our issued token

                actionContext.Response = actionContext.Request
                     .CreateResponse(HttpStatusCode.Unauthorized,
                     new { error = JwtStatusResult.InvalidToken });
                return;
            }

            var userId = HttpContext.Current.User.Identity.GetUserId();

            var serialNumber = HttpContext.Current.User.Identity.GetSerialNumber();
            if (serialNumber == null)
            {
                actionContext.Response = actionContext.Request
                     .CreateResponse(HttpStatusCode.Unauthorized,
                     new { error = JwtStatusResult.InvalidToken });
                return;
            }

            var validationResult = _userTokenService().ValidateToken(accessToken,
                serialNumber, userId);

            if (validationResult != null)
            {
                // this is not our issued token
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized,
                     new { error = validationResult });
            }

            base.OnAuthorization(actionContext);
        }

        // allow anonymous
        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor
                   .GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}