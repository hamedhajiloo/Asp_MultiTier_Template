using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj.Domain.User
{
    /// <summary>
    /// توکن‌های کاربر
    /// </summary>
    [Table("User_Tokens")]
    public class UserToken
    {
        public long Id { get; set; }

        /// <summary>
        /// کاربر
        /// </summary>
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(1000)]
        public string AccessTokenHash { get; set; }

        public DateTime? AccessTokenExpirationDateUtc { get; set; }

        [StringLength(1000)]
        public string RefreshTokenIdHash { get; set; }

        public DateTime? RefreshTokenExpirationDateUtc { get; set; }

        [StringLength(1000)]
        public string RefreshToken { get; set; }

        [StringLength(1000)]
        public string Device { get; set; }

        [StringLength(100)]
        public string UserIPAddress { get; set; }

        public string UserName { get; set; }
    }
}