using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Users
{
    /// <summary>
    /// شبکه‌های اجتماعی کاربر
    /// </summary>
    [Table("User_SocialNetworkList")]
    public class UserSocialNetworkList
    {
        public long Id { get; set; }


        /// <summary>
        /// شبکه اجتماعی
        /// </summary>
        public long SocialNetworkId { get; set; }
        public virtual UserSocialNetwork SocialNetwork { get; set; }


        /// <summary>
        /// کاربر
        /// </summary>
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }


        /// <summary>
        /// لینک کانال یا آیدی در شبکه اجتماعی مربوطه
        /// </summary>
        [Required]
        public string Link { get; set; }


        /// <summary>
        /// توضیحات مربوطه
        /// </summary>
        public string Description { get; set; }

    }
}
