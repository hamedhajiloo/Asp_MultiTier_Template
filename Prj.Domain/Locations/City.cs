using Prj.Domain.Enums;
using Prj.Domain.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Locations
{
    /// <summary>
    /// شهر ها
    /// </summary>
    [Table("Location_Cities")]
    public class City
    {
        public int Id { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// وضعیت فعال بودن
        /// </summary>
        public Active Active { get; set; }

        /// <summary>
        /// استان
        /// </summary>
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
