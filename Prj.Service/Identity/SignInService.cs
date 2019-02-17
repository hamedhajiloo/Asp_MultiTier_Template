using System.Security.Claims;
using System.Threading.Tasks;
using Prj.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Prj.Services.Identity
{
    public class SignInService :
        SignInManager<User, string>,
        ISignInService
    {
        private readonly IUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;

        public SignInService(
            IUserManager userManager,
            IAuthenticationManager authenticationManager) :
            base((UserManager)userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return _userManager.GenerateUserIdentityAsync(user);
        }

        /// <summary>
        /// How to refresh authentication cookies
        /// </summary>
        public async Task RefreshSignInAsync(User user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            // await _userManager.UpdateSecurityStampAsync(user.Id).ConfigureAwait(false); // = used for SignOutEverywhere functionality
            var claimsIdentity = await _userManager.GenerateUserIdentityAsync(user).ConfigureAwait(false);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, claimsIdentity);
        }
    }
}