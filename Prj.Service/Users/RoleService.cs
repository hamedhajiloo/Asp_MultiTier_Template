using Prj.DataAccess.Context;
using Prj.Domain.Users;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.Services.Users
{
    public interface IRoleService
    {
        Task<Role> FindByRoleNameAsync(string roleName, bool asNoTracking = true);
    }

    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Role> _roles;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roles = unitOfWork.Set<Role>();
        }

        public async Task<Role> FindByRoleNameAsync(string roleName, bool asNoTracking = true)
        {
            var query = _roles.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
