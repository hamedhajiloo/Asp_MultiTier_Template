using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Users
{
    /// <summary>
    /// شبکه‌های اجتماعی
    /// </summary>
    [Table("User_SocialNetworks")]
    public class UserSocialNetwork
    {
        public long Id { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        [StringLength(255)]
        public string Title { get; set; }


        /// <summary>
        /// توضیحات
        /// </summary>
        [StringLength(255)]
        public string Description { get; set; }


        /// <summary>
        /// آدرس سایت
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// شبکه‌های اجتماعی
        /// </summary>
        public virtual IList<UserSocialNetworkList> UserSocialNetworks { get; set; }
        
    }
}
