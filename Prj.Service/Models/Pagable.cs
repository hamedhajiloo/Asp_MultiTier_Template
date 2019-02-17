namespace Prj.Services.Models
{
    public class Pagable
    {
        /// <summary>
        /// صفحه
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// تعداد در هر صفحه
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// فیلد مرتب سازی
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// نزولی
        /// </summary>
        public bool Desc { get; set; } = true;

        /// <summary>
        /// جستجو
        /// </summary>
        public string Search { get; set; } = "";
    }
}
