using System.Data.Entity;
using Prj.DataAccess.Context;
using Prj.Domain.Users;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Prj.Services.Identity
{
    public class RoleStoreService :
        RoleStore<Role, string, UserRole>,
        IRoleStoreService
    {
        private readonly IUnitOfWork _context;

        public RoleStoreService(IUnitOfWork context)
            : base((DbContext)context)
        {
            _context = context;
        }
    }
}