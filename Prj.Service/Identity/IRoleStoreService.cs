using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Prj.Domain.Users;

namespace Prj.Services.Identity
{
    public interface IRoleStoreService : IDisposable
    {
        Task<Role> FindByIdAsync(string roleId);
        Task<Role> FindByNameAsync(string roleName);
        Task CreateAsync(Role role);
        Task DeleteAsync(Role role);
        Task UpdateAsync(Role role);
        DbContext Context { get; }
        bool DisposeContext { get; set; }
        IQueryable<Role> Roles { get; }
    }
}