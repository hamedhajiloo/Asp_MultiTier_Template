using System.ComponentModel.DataAnnotations.Schema;
using Prj.Domain.Files;

namespace Prj.Domain.Users
{
    [Table("User_Pictures")]
    public class UserPicture
    {
        /// <summary>
        /// شناسه رابطه
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// شناسه تصویر
        /// </summary>
        public string PictureId { get; set; }
        public virtual Picture Picture { get; set; }


        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public string UserId { get; set; }
        public virtual User User { get; set; }


        /// <summary>
        /// تایپ تصویر کاربر
        /// </summary>
        public UserPictureType PictureType { get; set; }

    }
    public enum UserPictureType
    {
        /// <summary>
        /// آواتار کاربر
        /// </summary>
        Avatar = 1,
    }
}
