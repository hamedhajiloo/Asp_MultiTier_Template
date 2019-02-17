using Prj.Domain.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Supports
{
    /// <summary>
    /// خطاهای سایت
    /// </summary>
    [Table("ErrorLogs")]
    public class ErrorLog
    {
        public long Id { get; set; }

        public string Exception { get; set; }

        public string InnerException { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(100)]
        public string UserIPAddress { get; set; }

        [StringLength(1000)]
        public string Device { get; set; }

        public DateTime InsertDate { get; set; }

        [StringLength(1000)]
        public string Route { get; set; }

        [StringLength(1000)]
        public string Parameters { get; set; }
    }
}
