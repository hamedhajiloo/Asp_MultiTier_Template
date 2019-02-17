using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Prj.DataAccess.Context;
using Prj.Domain.Supports;
using Prj.Utilities;

namespace Prj.Services.Utilities
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ErrorLog> _errorLogs;

        public ErrorLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _errorLogs = unitOfWork.Set<ErrorLog>();
        }

        public void Add(ErrorLog errorLog)
        {
            _unitOfWork.MarkAsAdded(errorLog);
        }

        public void Remove(ErrorLog errorLog)
        {
            _unitOfWork.MarkAsDeleted(errorLog);
        }

        public void Update(ErrorLog errorLog)
        {
            _unitOfWork.MarkAsChanged(errorLog);
        }

        public async Task<List<ErrorLog>> GetAllAsync(Pager pager, bool asNoTracking = true)
        {
            var errorLogs = _errorLogs.Include(p=>p.User).AsQueryable();

            if (asNoTracking)
                errorLogs = errorLogs.AsNoTracking();

            return await errorLogs
                .OrderByDescending(e => e.Id)
                .Skip((pager.CurrentPage - 1) * pager.PageSize)
                .Take(pager.PageSize)
                .ToListAsync();
        }

        public async Task<ErrorLog> FindByIdAsync(long? id, bool asNoTracking = true)
        {
            var errorLogs = _errorLogs.Include(p=>p.User).AsQueryable();

            if (asNoTracking)
                errorLogs = errorLogs.AsNoTracking();

            return await errorLogs.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> CountAsync()
        {
            return await _errorLogs.CountAsync();
        }

        public async Task ClearAllAsync()
        {
            var errorLogs = await _errorLogs.ToListAsync();

            foreach (var item in errorLogs)
            {
                _unitOfWork.MarkAsDeleted(item);
            }
        }
    }
}
