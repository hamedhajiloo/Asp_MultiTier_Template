using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;
using System.Security.Claims;
using Prj.Services.Utilities;
using Prj.Services.Users;
using Prj.DataAccess.Context;
using Prj.Domain.Users;
using System.Web;

namespace Prj.Common.JsonWebToken
{
    /// <summary>
    /// With the refresh token the user does not need to login again and
    /// they can use refresh token to request a new authorization token.
    /// </summary>
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly Func<ISecurityService> _securityService;
        private readonly IAppJwtConfiguration _configuration;
        private readonly Func<IUserTokenService> _userTokenService;
        private readonly Func<IUnitOfWork> _unitOfWork;

        public RefreshTokenProvider(
            IAppJwtConfiguration configuration,
            Func<IUserTokenService> userTokenService,
            Func<ISecurityService> securityService,
            Func<IUnitOfWork> unitOfWork)
        {
            _configuration = configuration;
            _configuration.CheckArgumentNull(nameof(_configuration));

            _userTokenService = userTokenService;
            _userTokenService.CheckArgumentNull(nameof(_userTokenService));

            _securityService = securityService;
            _securityService.CheckArgumentNull(nameof(_securityService));

            _unitOfWork = unitOfWork;
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            CreateAsync(context).RunSynchronously();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString();

            var utcNow = DateTime.UtcNow;

            var data = context.Request.ReadFormAsync().Result;

            var token = new UserToken()
            {
                UserId = context.Ticket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserIPAddress = HttpContext.Current.Request.UserHostAddress,
                Device = HttpContext.Current.Request.UserAgent,
                RefreshTokenIdHash = _securityService().GetSha256Hash(refreshTokenId),
                RefreshTokenExpirationDateUtc =
                  utcNow.AddMinutes(Convert.ToDouble(_configuration.RefreshTokenExpirationMinutes)),
                AccessTokenExpirationDateUtc = utcNow.AddMinutes(Convert.ToDouble(_configuration.ExpirationMinutes)),
                UserName = context.Ticket.Identity.Name,
            };

            _unitOfWork().MarkAsAdded(token);
            
            // Refresh token handles should be treated as secrets and should be stored hashed
            context.Ticket.Properties.IssuedUtc = utcNow;
            context.Ticket.Properties.ExpiresUtc = token.RefreshTokenExpirationDateUtc;

            token.RefreshToken = context.SerializeTicket();

            await _unitOfWork().SaveChangesAsync();

            context.SetToken(refreshTokenId);

            context.Ticket.Identity.AddClaim(new Claim("RefreshTokenIdHash", token.RefreshTokenIdHash));
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            ReceiveAsync(context).RunSynchronously();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var hashedTokenId = _securityService().GetSha256Hash(context.Token);
            var refreshToken = _userTokenService().FindToken(hashedTokenId);
            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.RefreshToken);
                _userTokenService().RemoveToken(hashedTokenId);
            }
        }
    }
}