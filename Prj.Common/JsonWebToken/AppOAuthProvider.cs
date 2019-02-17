using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Prj.Domain.Users;
using Prj.Services.Models;
using Prj.DataAccess.Context;
using Prj.Services.Identity;
using Prj.Services.Extensions;
using Prj.Services.Users;
using Prj.Services.Security;

namespace Prj.Common.JsonWebToken
{
    /// <summary>
    /// پیاده سازی لاگین برنامه
    /// </summary>
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IUserTokenService> _tokenStoreService;
        private readonly Func<IUserManager> _userManager;
        private readonly ISecurityService _securityService;
        private readonly IAppJwtConfiguration _configuration;
        private readonly Func<IUnitOfWork> _unitOfWork;

        /// <summary>
        /// Using Func here, creates transient Service's in a singleton AppOAuthProvider
        /// </summary>
        public AppOAuthProvider(
            Func<IUserTokenService> tokenStoreService,
            Func<IUserManager> userManager,
            ISecurityService securityService,
            IAppJwtConfiguration configuration,
            Func<IUnitOfWork> unitOfWork)
        {
            _tokenStoreService = tokenStoreService;
            _tokenStoreService.CheckArgumentNull(nameof(_tokenStoreService));

            _userManager = userManager;
            _userManager.CheckArgumentNull(nameof(_userManager));

            _securityService = securityService;
            _securityService.CheckArgumentNull(nameof(_securityService));

            _configuration = configuration;
            _configuration.CheckArgumentNull(nameof(_configuration));

            _unitOfWork = unitOfWork;
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId != null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            // Change authentication ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);


            var userId = context.Ticket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            _unitOfWork().SaveChanges();

            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var data = await context.Request.ReadFormAsync();

            var user = await _userManager().FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError(JwtStatusResult.InvalidStats);
                context.Rejected();
                return;
            }

            var identity = await SetClaimsIdentity(context, user);

            context.Validated(identity);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult(0);
        }

        private async Task<ClaimsIdentity> SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, User user)
        {
            var identity = new ClaimsIdentity("JWT");

            // Can Get By User.Identity.GetUserName() Or User.Identity.Name
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            // to invalidate the token
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, user.SerialNumber));

            // custom data
            //identity.AddClaim(new Claim(ClaimTypes.UserData, user.Id));

            // Can Get By User.Identity.GetUserId()
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var data = context.Request.ReadFormAsync().Result;

            identity.AddClaim(new Claim("ApplicationVersion", data["version"] ?? ""));

            var roles = await _userManager().GetRolesAsync(user.Id);

            foreach (var roleItem in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleItem));
            }

            return identity;
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            var token = context;

            var refreshTokenClaim = context.Identity.FindFirst("RefreshTokenIdHash");

            _tokenStoreService().UpdateUserToken(context.Identity.GetUserId(),
                refreshTokenClaim.Value,
                _securityService.GetSha256Hash(context.AccessToken));

            context.Identity.RemoveClaim(refreshTokenClaim);

            _unitOfWork().SaveChanges();

            return base.TokenEndpointResponse(context);
        }
    }


}