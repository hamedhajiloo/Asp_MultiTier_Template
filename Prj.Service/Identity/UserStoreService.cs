using Prj.DataAccess.Context;
using Prj.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Prj.Services.Identity
{
    public class UserStoreService :
        UserStore<User, Role, string, UserLogin, UserRole, UserClaim>,
        IUserStoreService
    {
        private readonly IUnitOfWork _context;

        public UserStoreService(IUnitOfWork context)
            : base((AppDbContext)context)
        {
            _context = context;
        }

    }
}