using Prj.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Locations
{
    /// <summary>
    /// استان ها
    /// </summary>
    [Table("Location_Provinces")]
    public class Province
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

        public virtual List<City> Cities { get; set; }
    }
}
