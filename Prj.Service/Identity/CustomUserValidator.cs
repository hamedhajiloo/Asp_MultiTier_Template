using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prj.Domain.Users;

namespace Prj.Services.Identity
{
    public class CustomUserValidator<TUser, TKey> : IIdentityValidator<User>
        where TUser : class, IUser<string>
        where TKey : IEquatable<string>
    {

        public bool AllowOnlyAlphanumericUserNames { get; set; }
        public bool RequireUniqueEmail { get; set; }

        private UserManager _userManager { get; set; }
        public CustomUserValidator(UserManager userManager)
        {
            if (userManager == null)
                throw new ArgumentNullException("manager");
            AllowOnlyAlphanumericUserNames = true;
            _userManager = userManager;
        }
        public virtual async Task<IdentityResult> ValidateAsync(User item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            var errors = new List<string>();
            await ValidateUserName(item, errors);
            if (RequireUniqueEmail)
                await ValidateEmailAsync(item, errors);
            return errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

        private async Task ValidateUserName(User user, ICollection<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
                errors.Add("نام کاربری نباید خالی باشد");
            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, "^[A-Za-z0-9@_\\.]+$"))
            {
                errors.Add("برای نام کاربری فقط از کاراکتر‌های مجاز استفاده کنید ");
            }
            else
            {
                var owner = await _userManager.FindByNameAsync(user.UserName);
                if (owner != null && !EqualityComparer<string>.Default.Equals(owner.Id, user.Id))
                    errors.Add("این نام کاربری قبلا ثبت شده است");
            }
        }

        private async Task ValidateEmailAsync(User user, ICollection<string> errors)
        {
            var email = await _userManager.GetEmailStore().GetEmailAsync(user);//.WithCurrentCulture();
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("وارد کردن ایمیل ضروریست");
            }
            else
            {
                try
                {
                    var m = new MailAddress(email);

                }
                catch (FormatException)
                {
                    errors.Add("ایمیل را به شکل صحیح وارد کنید");
                    return;
                }
                var owner = await _userManager.FindByEmailAsync(email);
                if (owner != null && !EqualityComparer<string>.Default.Equals(owner.Id, user.Id))
                    errors.Add("این ایمیل قبلا ثبت شده است");
            }
        }
    }
}