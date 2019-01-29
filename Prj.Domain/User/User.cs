using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj.Domain.User
{
    /// <summary>
    /// کاربران
    /// </summary>
    public class User:IdentityUser<string,UserLogin,UserRole,UserClaim>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        public virtual List<UserToken> UserTokens { get; set; }
    }
}
