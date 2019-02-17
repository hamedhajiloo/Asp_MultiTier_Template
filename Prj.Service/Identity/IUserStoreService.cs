using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Prj.Domain.Users;
using Microsoft.AspNet.Identity;

namespace Prj.Services.Identity
{
    public interface IUserStoreService
    {
        Task<IList<Claim>> GetClaimsAsync(User user);
        Task AddClaimAsync(User user, Claim claim);
        Task RemoveClaimAsync(User user, Claim claim);
        Task<bool> GetEmailConfirmedAsync(User user);
        Task SetEmailConfirmedAsync(User user, bool confirmed);
        Task SetEmailAsync(User user, string email);
        Task<string> GetEmailAsync(User user);
        Task<User> FindByEmailAsync(string email);
        Task<DateTimeOffset> GetLockoutEndDateAsync(User user);
        Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd);
        Task<int> IncrementAccessFailedCountAsync(User user);
        Task ResetAccessFailedCountAsync(User user);
        Task<int> GetAccessFailedCountAsync(User user);
        Task<bool> GetLockoutEnabledAsync(User user);
        Task SetLockoutEnabledAsync(User user, bool enabled);
        Task<User> FindByIdAsync(string userId);
        Task<User> FindByNameAsync(string userName);
        Task CreateAsync(User user);
        Task DeleteAsync(User user);
        Task UpdateAsync(User user);
        void Dispose();
        Task<User> FindAsync(UserLoginInfo login);
        Task AddLoginAsync(User user, UserLoginInfo login);
        Task RemoveLoginAsync(User user, UserLoginInfo login);
        Task<IList<UserLoginInfo>> GetLoginsAsync(User user);
        Task SetPasswordHashAsync(User user, string passwordHash);
        Task<string> GetPasswordHashAsync(User user);
        Task<bool> HasPasswordAsync(User user);
        Task SetPhoneNumberAsync(User user, string phoneNumber);
        Task<string> GetPhoneNumberAsync(User user);
        Task<bool> GetPhoneNumberConfirmedAsync(User user);
        Task SetPhoneNumberConfirmedAsync(User user, bool confirmed);
        Task AddToRoleAsync(User user, string roleName);
        Task RemoveFromRoleAsync(User user, string roleName);
        Task<IList<string>> GetRolesAsync(User user);
        Task<bool> IsInRoleAsync(User user, string roleName);
        Task SetSecurityStampAsync(User user, string stamp);
        Task<string> GetSecurityStampAsync(User user);
        Task SetTwoFactorEnabledAsync(User user, bool enabled);
        Task<bool> GetTwoFactorEnabledAsync(User user);
        DbContext Context { get; }
        bool DisposeContext { get; set; }
        bool AutoSaveChanges { get; set; }
        IQueryable<User> Users { get; }
    }
}