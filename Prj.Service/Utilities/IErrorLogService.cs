using System.Collections.Generic;
using System.Threading.Tasks;
using Prj.Domain.Supports;
using Prj.Utilities;

namespace Prj.Services.Utilities
{
    public interface IErrorLogService
    {
        void Add(ErrorLog errorLog);
        Task<int> CountAsync();
        Task<ErrorLog> FindByIdAsync(long? id, bool asNoTracking = true);
        Task<List<ErrorLog>> GetAllAsync(Pager pager, bool asNoTracking = true);
        void Remove(ErrorLog errorLog);
        void Update(ErrorLog errorLog);
        Task ClearAllAsync();

    }
}