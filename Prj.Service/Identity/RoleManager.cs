using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Prj.DataAccess.Context;
using Prj.Domain.Users;
using Microsoft.AspNet.Identity;

namespace Prj.Services.Identity
{
    public class RoleManager :
        RoleManager<Role, string>,
        IRoleManager
    {
        private readonly IUnitOfWork _uow;
        private readonly IRoleStoreService _roleStore;
        private readonly IDbSet<User> _users;
        public RoleManager(
            IUnitOfWork uow,
            IRoleStoreService roleStore)
            : base((IRoleStore<Role, string>)roleStore)
        {
            _uow = uow;
            _roleStore = roleStore;
            _users = _uow.Set<User>();
        }

        public Role FindRoleByName(string roleName)
        {
            return this.FindByName(roleName); // RoleManagerExtensions
        }

        public IdentityResult CreateRole(Role role)
        {
            return this.Create(role); // RoleManagerExtensions
        }

        public IList<UserRole> GetCustomUsersInRole(string roleName)
        {
            return this.Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.Users)
                             .ToList();
            // = this.FindByName(roleName).Users
        }

        public IList<User> GetApplicationUsersInRole(string roleName)
        {
            var roleUserIdsQuery = from role in this.Roles
                                  where role.Name == roleName
                                  from user in role.Users
                                  select user.UserId;
            return _users.Where(applicationUser => roleUserIdsQuery.Contains(applicationUser.Id))
                         .ToList();
        }

        public IList<Role> FindUserRoles(string userId)
        {
            var userRolesQuery = from role in this.Roles
                        from user in role.Users
                        where user.UserId == userId
                        select role;

            return userRolesQuery.OrderBy(x => x.Name).ToList();
        }

        public string[] GetRolesForUser(string userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new string[] { };
            }

            return roles.Select(x => x.Name).ToArray();
        }

        public bool IsUserInRole(string userId, string roleName)
        {
            var userRolesQuery = from role in this.Roles
                        where role.Name == roleName
                        from user in role.Users
                        where user.UserId == userId
                        select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        public Task<List<Role>> GetAllCustomRolesAsync()
        {
            return this.Roles.ToListAsync();
        }
    }
}