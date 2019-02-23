using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Prj.Web.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                string messages = string.Join("\n", actionContext.ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage));

                throw new System.Exception("ورودی نامعتبر" + "\n" + messages);
            }
                
        }
    }

}
