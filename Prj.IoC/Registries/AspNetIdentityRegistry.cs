using StructureMap;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StructureMap.Web;
using Prj.DataAccess.Context;
using Prj.Services.Identity;
using Prj.Domain.Users;

namespace Prj.IocConfig.Registries
{
    public class AspNetIdentityRegistry : Registry
    {
        public AspNetIdentityRegistry()
        {
            For<IIdentity>()
                .HybridHttpOrThreadLocalScoped()
                .Use(() => GetIdentity());

            For<IUnitOfWork>()
                .HybridHttpOrThreadLocalScoped()
                .Use<AppDbContext>();

            For<AppDbContext>()
               .HybridHttpOrThreadLocalScoped()
               .Use(context => (AppDbContext)context.GetInstance<IUnitOfWork>());
            For<DbContext>()
               .HybridHttpOrThreadLocalScoped()
               .Use(context => (AppDbContext)context.GetInstance<IUnitOfWork>());

            For<IUserStore<User, string>>()
                .HybridHttpOrThreadLocalScoped()
                .Use<UserStoreService>();

            For<IRoleStore<Role, string>>()
                .HybridHttpOrThreadLocalScoped()
                .Use<RoleStoreService>();

            For<IAuthenticationManager>()
               .HybridHttpOrThreadLocalScoped()
               .Use(() => HttpContext.Current.GetOwinContext().Authentication);

            For<ISignInService>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<SignInService>();

            For<IRoleManager>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<RoleManager>();

            // map same interface to different concrete classes
            For<IIdentityMessageService>().Use<SmsService>();
            For<IIdentityMessageService>().Use<EmailService>();

            For<IUserManager>()
               .HybridHttpOrThreadLocalScoped()
               .Use<UserManager>()
               .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
               .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
               .Setter(userManager => userManager.SmsService).Is<SmsService>()
               .Setter(userManager => userManager.EmailService).Is<EmailService>();

            For<UserManager>()
               .HybridHttpOrThreadLocalScoped()
               .Use(context => (UserManager)context.GetInstance<IUserManager>());

            For<IRoleStoreService>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<RoleStoreService>();

            For<IUserStoreService>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<UserStoreService>();
        }

        private static IIdentity GetIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity;
            }

            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
        }

    }


}
