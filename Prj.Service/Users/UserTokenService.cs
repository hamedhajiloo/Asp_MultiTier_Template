using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Prj.Domain.Users;
using Prj.Services.Utilities;
using Prj.DataAccess.Context;
using Prj.Services.Models;

namespace Prj.Services.Users
{
    public interface IUserTokenService
    {
        void CreateUserToken(UserToken userToken);
        void RemoveExpiredTokens();
        UserToken FindToken(string refreshTokenIdHash);
        void RemoveToken(string refreshTokenIdHash);
        void InvalidateUserTokens(string userId);
        void UpdateUserToken(string userId, string refreshTokenHash, string accessTokenHash);
        Task<UserToken> FindToken(string userName, string password, string accessToken);
        Task RemoveToken(string userId, string accessToken);
        string ValidateToken(string accessToken, string serialNumber, string userId);
        Task<UserToken> FindTokenAsync(string userId);
    }

    public class UserTokenService : IUserTokenService
    {
        private readonly IDbSet<UserToken> _tokens;
        private readonly IDbSet<User> _users;
        private readonly ISecurityService _securityService;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IUnitOfWork _unitOfWork;

        public UserTokenService(IUnitOfWork unitOfWork,
            ISecurityService securityService,
            IPasswordHasherService passwordHasherService)
        {
            _unitOfWork = unitOfWork;
            _securityService = securityService;
            _passwordHasherService = passwordHasherService;
            _tokens = _unitOfWork.Set<UserToken>();
            _users = _unitOfWork.Set<User>();
        }

        public void CreateUserToken(UserToken userToken)
        {
            InvalidateUserTokens(userToken.UserId);
            _tokens.Add(userToken);
        }

        public void UpdateUserToken(string userId, string refreshTokenHash, string accessTokenHash)
        {
            var token = _tokens.FirstOrDefault(x => x.UserId == userId && x.RefreshTokenIdHash == refreshTokenHash);
            token.AccessTokenHash = accessTokenHash;
        }

        public void RemoveExpiredTokens()
        {
            var now = DateTime.UtcNow;
            var userExpiredTokens = _tokens.Where(x => x.AccessTokenExpirationDateUtc < now).ToList();
            foreach (var userToken in userExpiredTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        public void RemoveToken(string refreshTokenIdHash)
        {
            var token = FindToken(refreshTokenIdHash);
            if (token != null)
            {
                _tokens.Remove(token);
            }
        }

        public UserToken FindToken(string refreshTokenIdHash)
        {
            return _tokens.FirstOrDefault(x => x.RefreshTokenIdHash == refreshTokenIdHash);
        }

        public void InvalidateUserTokens(string userId)
        {
            var userTokens = _tokens.Where(x => x.UserId == userId).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        public async Task<UserToken> FindToken(string userName, string password, string accessToken)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);

            var token = await _tokens.Include(u => u.User)
                .Where(u => u.User.UserName == userName && u.AccessTokenHash == accessTokenHash)
                .FirstOrDefaultAsync();

            if (token != null)
            {
                if (_passwordHasherService.VerifyHashedPassword(token.User.PasswordHash,
                    password) == PasswordVerificationResult.Success)
                {
                    return token;
                }
            }

            return null;
        }

        public async Task RemoveToken(string userId, string accessToken)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);

            var token = await _tokens.Where(t => t.AccessTokenHash == accessTokenHash && t.UserId == userId).FirstOrDefaultAsync();
            _unitOfWork.MarkAsDeleted(token);
        }

        public string ValidateToken(string accessToken, string serialNumber, string userId)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);
            var userToken = _tokens.Include(t => t.User).AsNoTracking()
                .FirstOrDefault(x => x.AccessTokenHash == accessTokenHash && x.UserId == userId);

            if (userToken == null)
            {
                return JwtStatusResult.InvalidToken;
            }

            if (userToken.AccessTokenExpirationDateUtc < DateTime.UtcNow)
            {
                return JwtStatusResult.InvalidToken;
            }

            if (userToken.User.SerialNumber != serialNumber)
            {
                return JwtStatusResult.InvalidStats;
            }

            return null;
        }

        public async Task<UserToken> FindTokenAsync(string userId)
        {
            var dtNow = DateTime.Now;
            return await _tokens.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}