using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Supports
{
    /// <summary>
    /// کل وب‌ سرویس‌ها و تعداد بار فراخوانی شده
    /// </summary>
    [Table("WebMethods")]
    public class WebMethod
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Controller { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        public int CalledCount { get; set; }

        public string LastParameters { get; set; }

    }
}
