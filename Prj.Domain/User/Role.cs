using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj.Domain.Users
{
    /// <summary>
    /// نقش
    /// </summary>
    public class Role : IdentityRole<string, UserRole>
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Role(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        /// <summary>
        /// نام فارسی
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string FaName { get; set; }

    }
}
