using System.Threading.Tasks;
using System.Collections.Generic;
using Prj.Domain.Supports;

namespace Prj.Services.Utilities
{
    public interface IWebMethodService
    {
        Task<WebMethod> FindAsync(string controllerName, string actionName);
        void Add(string controllerName, string actionName, int count, string lastParameters);
        Task PushAllMethodsToDatabaseAsync();
        Task<List<WebMethod>> GetAllAsync(bool asNoTracking = true);
        Task ClearAllAsync();

    }
}