using Microsoft.AspNet.Identity.EntityFramework;
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
    /// نقش های کاربران
    /// </summary>
    public class UserRole : IdentityUserRole<string>
    {
        /// <summary>
        /// کاربر
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public override string UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// نقش
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public override string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
