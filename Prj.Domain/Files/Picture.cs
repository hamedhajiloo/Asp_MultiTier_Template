using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Prj.Domain.Users;
using Prj.Domain.Enums;

namespace Prj.Domain.Files
{
    /// <summary>
    /// جدول رسانه
    /// </summary>
    [Table("Pictures")]
    public class Picture
    {
        /// <summary>
        /// سازنده پیشفرض
        /// </summary>
        public Picture()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// شناسه رسانه
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// نام رسانه
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// مسیر فایل
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// زمان ایجاد
        /// </summary>
        public DateTime WriteTime { get; set; }

        /// <summary>
        /// تصدیق شده
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// توضیحات عدم تایید
        /// </summary>
        public string RejectedDescription { get; set; }



        #region User Relation

        /// <summary>
        /// تصاویر پروفایل عکس
        /// </summary>
        public virtual IList<UserPicture> UserPictures { get; set; }

        #endregion

      

    }
}
