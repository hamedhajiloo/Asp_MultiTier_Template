using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prj.Domain.Users;
using Microsoft.AspNet.Identity;

namespace Prj.Services.Identity
{
    public interface IRoleManager : IDisposable
    {
        /// <summary>
        /// Used to validate roles before persisting changes
        /// </summary>
        IIdentityValidator<Role> RoleValidator { get; set; }

        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> CreateAsync(Role role);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> UpdateAsync(Role role);

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> DeleteAsync(Role role);

        /// <summary>
        /// Returns true if the role exists
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<bool> RoleExistsAsync(string roleName);

        /// <summary>
        /// Find a role by id
        /// </summary>
        /// <param name="roleId"/>
        /// <returns/>
        Task<Role> FindByIdAsync(string roleId);

        /// <summary>
        /// Find a role by name
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<Role> FindByNameAsync(string roleName);


        // Our new custom methods

        Role FindRoleByName(string roleName);
        IdentityResult CreateRole(Role role);
        IList<UserRole> GetCustomUsersInRole(string roleName);
        IList<User> GetApplicationUsersInRole(string roleName);
        IList<Role> FindUserRoles(string userId);
        string[] GetRolesForUser(string userId);
        bool IsUserInRole(string userId, string roleName);
        Task<List<Role>> GetAllCustomRolesAsync();
    }
}