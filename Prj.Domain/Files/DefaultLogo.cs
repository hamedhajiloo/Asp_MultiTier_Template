using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Files
{
    [Table("Files_DefaultLogo")]
    public class DefaultLogo
    {
        public int Id { get; set; }


        /// <summary>
        /// آواتار کاربر
        /// </summary>
        [Display(Name = "آواتار کاربر")]
        [Required(ErrorMessage = "{0} را انتخاب کنید")]
        public string UserAvatarUrl { get; set; }

        /// <summary>
        /// آواتار مشتری
        /// </summary>
        [Display(Name = "آواتار مشتری")]
        [Required(ErrorMessage = "{0} را انتخاب کنید")]
        public string CustomerAvatarUrl { get; set; }

        /// <summary>
        /// آواتار مالک
        /// </summary>
        [Display(Name = "آواتار مالک")]
        [Required(ErrorMessage = "{0} را انتخاب کنید")]
        public string ShopOwnerAvatarUrl { get; set; }

        /// <summary>
        /// لوگو فروشگاه
        /// </summary>
        [Display(Name = "لوگو فروشگاه")]
        [Required(ErrorMessage = "{0} را انتخاب کنید")]
        public string ShopLogoUrl { get; set; }

        /// <summary>
        /// تصویر اصلی فروشگاه
        /// </summary>
        [Display(Name = "تصویر اصلی فروشگاه")]
        [Required(ErrorMessage = "{0} را انتخاب کنید")]
        public string ShopMainImageUrl { get; set; }

        /// <summary>
        /// لودینگ
        /// </summary>
        [Display(Name = "لودینگ")]
        [Required(ErrorMessage = "{0} را انتخاب کنید")]
        public string Loading { get; set; }

        /// <summary>
        /// دسته بندی
        /// </summary>
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "{0} را انتخاب کنید")]
        public string Category { get; set; }
    }
}
