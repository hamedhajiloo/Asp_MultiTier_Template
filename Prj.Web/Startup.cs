using Prj.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin;
using Owin;
using StructureMap.Web;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Optimization;
using Prj.Services.Identity;
using Microsoft.Owin.Security.Jwt;
using Prj.Common.JsonWebToken;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;
using System.Text;
using Prj.IoC;

namespace Prj.Web
{
    //[assembly: OwinStartupAttribute(typeof(Prj.Web.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            configureAuth(app);
        }

        private static void configureAuth(IAppBuilder app)
        {
            var container = SmObjectFactory.Container;

            app.UseOAuthAuthorizationServer(SmObjectFactory.Container.GetInstance<AppOAuthOptions>());
            app.UseJwtBearerAuthentication(SmObjectFactory.Container.GetInstance<AppJwtOptions>());

            container.Configure(config =>
            {
                config.For<IDataProtectionProvider>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use(() => app.GetDataProtectionProvider());
            });
            container.GetInstance<IUserManager>().SeedDatabase();

            // This is necessary for `GenerateUserIdentityAsync` and `SecurityStampValidator` to work internally by ASP.NET Identity 2.x
            app.CreatePerOwinContext(() => (UserManager)container.GetInstance<IUserManager>());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieName = ".Bmeh.ir.Provider",
                ExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.
                    OnValidateIdentity = context =>
                    {
                        if(shouldIgnoreRequest(context)) // How to ignore Authentication Validations for static files in ASP.NET Identity
                        {
                            return Task.FromResult(0);
                        }
                        return container.GetInstance<IUserManager>().OnValidateIdentity().Invoke(context);
                    }
                },
                SlidingExpiration = false,
                CookieManager = new SystemWebCookieManager()
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.MapSignalR();
        }

        private static bool shouldIgnoreRequest(CookieValidateIdentityContext context)
        {
            string[] reservedPath =
            {
                "/__browserLink",
                "/img",
                "/fonts",
                "/Scripts",
                "/Content",
                "/Uploads",
                "/Images",
                "/Account/Login"
            };
            return reservedPath.Any(path => context.OwinContext.Request.Path.Value.StartsWith(path, StringComparison.OrdinalIgnoreCase)) ||
                               BundleTable.Bundles.Select(bundle => bundle.Path.TrimStart('~')).Any(bundlePath => context.OwinContext.Request.Path.Value.StartsWith(bundlePath,StringComparison.OrdinalIgnoreCase));
        }
    }

    public class AppOAuthOptions : OAuthAuthorizationServerOptions
    {
        public AppOAuthOptions(IAppJwtConfiguration configuration)
        {
            AllowInsecureHttp = true; // TODO: Buy an SSL certificate!
            TokenEndpointPath = new PathString(configuration.TokenPath);
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(configuration.ExpirationMinutes);
            AccessTokenFormat = new AppJwtWriterFormat(this, configuration);
            Provider = SmObjectFactory.Container.GetInstance<IOAuthAuthorizationServerProvider>();
            RefreshTokenProvider = SmObjectFactory.Container.GetInstance<IAuthenticationTokenProvider>();
        }
    }

    public class AppJwtOptions : JwtBearerAuthenticationOptions
    {
        public AppJwtOptions(IAppJwtConfiguration config)
        {
            this.AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active;
            this.AllowedAudiences = new[] { config.JwtAudience };
            this.IssuerSecurityKeyProviders = new[]
            {
                new SymmetricKeyIssuerSecurityKeyProvider(
                    issuer: config.JwtIssuer,
                    base64Key: Convert.ToBase64String(Encoding.UTF8.GetBytes(config.JwtKey)))
            };
        }
    }
}