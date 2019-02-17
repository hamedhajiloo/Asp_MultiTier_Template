using System.Data.Entity;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;
using Prj.DataAccess.Context;
using Prj.Domain.Supports;

namespace Prj.Services.Utilities
{
    public class WebMethodService : IWebMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<WebMethod> _webMethods;

        public WebMethodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _webMethods = unitOfWork.Set<WebMethod>();
        }

        public void Add(string controllerName, string actionName, int count, string lastParameters)
        {
            _webMethods.Add(new WebMethod()
            {
                Controller = controllerName,
                Action = actionName,
                CalledCount = count,
                LastParameters = lastParameters
            });
        }

        public async Task<WebMethod> FindAsync(string controllerName, string actionName)
        {
            return await _webMethods.FirstOrDefaultAsync(u => u.Controller == controllerName && u.Action == actionName);
        }

        public async Task PushAllMethodsToDatabaseAsync()
        {
            Assembly asm = Assembly.Load("Prj.Web");

            var data = asm.GetTypes()
                .Where(type => typeof(ApiController).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly |
                                                    BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    true).Any())
                .Select(x => new
                {
                    Controller = x.DeclaringType?.Name,
                    Action = x.Name,
                })
                .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            var webMethods = await this.GetAllAsync();

            foreach (var item in data)
            {
                var controller = item.Controller.Replace("Controller", ""); //to remove controller from the end of controller name

                if (!webMethods.Any(w => w.Controller == controller && w.Action == item.Action))
                {
                    _unitOfWork.MarkAsAdded(new WebMethod()
                    {
                        Controller = controller,
                        Action = item.Action
                    });
                }
            }
        }

        public async Task<List<WebMethod>> GetAllAsync(bool asNoTracking = true)
        {
            var webMethods = _webMethods.AsQueryable();

            if (asNoTracking)
                webMethods = webMethods.AsNoTracking();

            return await webMethods.ToListAsync();
        }

        public async Task ClearAllAsync()
        {
            var models = await _webMethods.ToListAsync();
            foreach (var item in models)
            {
                _unitOfWork.MarkAsDeleted(item);
            }
        }
    }
}
