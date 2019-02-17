using Prj.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Users
{
    /// <summary>
    /// کد فعال ساز کاربر
    /// </summary>
    [Table("User_ActiveCodes")]
    public class UserActiveCode
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// تلفن
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// کد فعال سازی
        /// </summary>
        public int ActiveCode { get; set; }

        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        public DateTime InsertDate { get; set; }

        /// <summary>
        /// نوع کد فعال ساز
        /// </summary>
        public ActiveCodeType ActiveCodeType { get; set; }

        /// <summary>
        /// استفاده شده
        /// </summary>
        public bool Used { get; set; }

        /// <summary>
        /// تاریخ انقضاء استفاده
        /// </summary>
        public DateTime UsedExpireDate { get; set; }

        /// <summary>
        /// ثبت نام شده؟
        /// </summary>
        public bool Registered { get; set; }

        /// <summary>
        /// تاریخ انقضاء ثبت نام
        /// </summary>
        public DateTime RegisterExpireDate { get; set; }

        /// <summary>
        /// وضعیت فعال بودن
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// آدرس آی پی
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// وسیله
        /// </summary>
        public string Device { get; set; }
    }
}
