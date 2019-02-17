using Microsoft.AspNet.Identity.EntityFramework;
using Prj.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj.Domain.Users
{
    /// <summary>
    /// کاربران
    /// </summary>
    public class User : IdentityUser<string, UserLogin, UserRole, UserClaim>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// شماره دانشجویی یا شماره استاد یا تلفن همراه
        /// </summary>
        [Display(Name = "نام کاربری")]
        public override string UserName { get; set; }

        /// <summary>
        /// ایمیل
        /// </summary>
        [Display(Name = "ایمیل")]
        public override string Email { get; set; }

        /// <summary>
        /// تلفن همراه
        /// </summary>
        [Display(Name = "تلفن همراه")]
        [RegularExpression(@"((09)(\d{9}))", ErrorMessage = "{0} خود را درست وارد نمایید")]
        [Required]
        public override string PhoneNumber { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// نام نمایشی
        /// </summary>
        public string DisplayName { get; set; }


        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// کد پستی
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// شماره کارت بانکی
        /// </summary>
        public string BankCardNumber { get; set; }

        /// <summary>
        /// آواتار کاربر
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public string NationalId { get; set; }

        /// <summary>
        /// عضویت در خبر نامه
        /// </summary>
        public bool NewsLetter { get; set; }

        /// <summary>
        /// سریال مربوط به توکن کاربر
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// آخرین تاریخ ورود
        /// </summary>
        public DateTime? LastLoggedIn { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// شارژ کیف پول
        /// </summary>
        [Range(0, int.MaxValue)]
        public long WalletCharge { get; set; }

        /// <summary>
        /// تاریخ عضویت
        /// </summary>
        [Display(Name = "تاریخ عضویت")]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// قفل شده
        /// </summary>
        [Display(Name = "قفل شده")]
        public override bool LockoutEnabled { get; set; }


        public virtual List<UserToken> UserTokens { get; set; }
        public IList<UserPicture> UserPictures { get; set; }
        public IList<UserSocialNetworkList> UserSocialNetworks { get; set; }
        public IList<UserCart> UserCarts { get; set; }
    }
}
