using System.Web.Http.Controllers;
using System.Web.Http;

namespace Prj.Web.Attributes
{
    /// <summary>
    /// </summary>
    public class OptionalAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// </summary>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.IsLocal)
                return;

            base.OnAuthorization(actionContext);
        }
    }
}