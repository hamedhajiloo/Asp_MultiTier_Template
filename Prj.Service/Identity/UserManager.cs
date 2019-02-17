using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Prj.DataAccess.Context;
using Prj.Domain.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Prj.Utilities;
using System.Linq;

namespace Prj.Services.Identity
{
    public class UserManager
        : UserManager<User, string>,
        IUserManager
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IRoleManager _roleManager;
        private readonly IUserStoreService _store;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<User> _users;
        private readonly Lazy<Func<IIdentity>> _identity;
        private User _user;

        public UserManager(
            IUserStoreService store,
            IUnitOfWork uow,
            Lazy<Func<IIdentity>> identity, // For lazy loading -> Controller gets constructed before the HttpContext has been set by ASP.NET.
            IRoleManager roleManager,
            IDataProtectionProvider dataProtectionProvider,
            IIdentityMessageService smsService,
            IIdentityMessageService emailService)
            : base((IUserStore<User, string>)store)
        {
            _store = store;
            _uow = uow;
            _identity = identity;
            _users = _uow.Set<User>();
            _roleManager = roleManager;
            _dataProtectionProvider = dataProtectionProvider;
            this.SmsService = smsService;
            this.EmailService = emailService;

            createApplicationUserManager();
        }

        public User FindById(string userId)
        {
            return _users.Find(userId);
        }

        public IUserEmailStore<User, string> GetEmailStore()
        {
            var cast = Store as IUserEmailStore<User, string>;
            if (cast == null)
            {
                throw new NotSupportedException("not support");
            }
            return cast;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim("user-email", applicationUser.Email ?? ""));
            userIdentity.AddClaim(new Claim("user-fullName",  applicationUser.FirstName + " " + applicationUser.LastName));

            var avatar = applicationUser.Avatar ?? Url.DefaultUrl;
            userIdentity.AddClaim(new Claim("user-avatar", avatar));

            //userIdentity.AddClaim(new Claim("user-messagesCount", applicationUser.UserMessageReceives.Count(p=>!p.Viewed).ToString()));
            //userIdentity.AddClaim(new Claim("user-notificationsCount", applicationUser.UserNotifications.Count(p => !p.Viewed).ToString()));

            return userIdentity;
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return this.Users.ToListAsync();
        }

        public User GetCurrentUser()
        {
            return _user ?? (_user = this.FindById(GetCurrentUserId()));
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return _user ?? (_user = await this.FindByIdAsync(GetCurrentUserId()).ConfigureAwait(false));
        }

        public string GetCurrentUserId()
        {
            return _identity.Value().GetUserId<string>();
        }

        public async Task<bool> HasPassword(string userId)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            return user != null && user.PasswordHash != null;
        }

        public async Task<bool> HasPhoneNumber(string userId)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            return user != null && user.PhoneNumber != null;
        }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return SecurityStampValidator.OnValidateIdentity<UserManager, User, string>(
                         validateInterval: TimeSpan.FromSeconds(0),
                         regenerateIdentityCallback: (manager, user) => manager.GenerateUserIdentityAsync(user),
                         getUserIdCallback: claimsIdentity => claimsIdentity.GetUserId<string>());
        }

        public void SeedDatabase()
        {
            const string name = "admin";
            const string password = "Admin@123456";
            const string roleName = "Administrator";

            //Create Role Administrator if it does not exist
            var role = _roleManager.FindRoleByName(roleName);
            if (role == null)
            {
                role = new Role(roleName);
                var roleResult = _roleManager.CreateRole(role);
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(", ", roleResult.Errors));
                }
            }

            var user = this.FindByName(name);
            if (user == null)
            {
                user = new User
                {
                    UserName = name,
                    PhoneNumber = "09000000000",
                    SerialNumber = Guid.NewGuid().ToString(),
                    RegisterDate = DateTime.Now,
                };

                var createResult = this.Create(user, password);
                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(", ", createResult.Errors));
                }

                var setLockoutResult = this.SetLockoutEnabled(user.Id, false);
                if (!setLockoutResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(", ", setLockoutResult.Errors));
                }
            }

            // Add user Administrator to Role Admin if not already added
            var rolesForUser = this.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var addToRoleResult = this.AddToRole(user.Id, role.Name);
                if (!addToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(", ", addToRoleResult.Errors));
                }
            }
        }

        private void createApplicationUserManager()
        {
            // Configure validation logic for usernames
            this.UserValidator = new CustomUserValidator<User, string>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User, string>
            {
                MessageFormat = "Your security code is: {0}"
            });
            this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, string>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });

            if (_dataProtectionProvider != null)
            {
                var dataProtector = _dataProtectionProvider.Create("ASP.NET Identity");
                this.UserTokenProvider = new DataProtectorTokenProvider<User, string>(dataProtector);
            }
        }
    }
}