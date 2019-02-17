using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Prj.Services.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            if (identity is ClaimsIdentity ci)
            {
                return ci.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            }
            return null;
        }

        public static string GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            if (identity is ClaimsIdentity ci)
            {
                return ci.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return null;
        }

        public static string GetEmail(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            return ci?.FindFirstValue(ClaimTypes.Email);
        }

        public static string GetSerialNumber(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            return ci?.FindFirstValue(ClaimTypes.SerialNumber);
        }

        public static ClaimsIdentity GetClaimIdentity(this IIdentity identity)
        {
            return HttpContext.Current.User.Identity as ClaimsIdentity;
        }

        public static string GetApplicationVersion(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            return ci?.FindFirstValue("ApplicationVersion");
        }

        public static IEnumerable<string> GetRoles(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            return ci?.FindAll(ClaimTypes.Role).Select(item => item.Value).ToList();
        }

        private static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var claim = identity.FindFirst(claimType);
            return claim?.Value;
        }
    }
}
