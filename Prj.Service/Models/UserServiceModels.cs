using Prj.Domain.Enums;
using Prj.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prj.Services.Models
{

    /// <summary>
    /// ویو مدل دریافتی ارتباطات به همراه مقادیر آنها
    /// </summary>
    public class UserCommunicationServiceModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class UserCartDetailsServiceModel
    {
        public IList<UserProductInventoryServiceModel> ProductInventories { get; set; }

        public UserProductInventoryPriceAllModel Total { get; set; }

        public class UserProductInventoryServiceModel
        {
            public long ProductId { get; set; }
            public string Name { get; set; }
            public string Model { get; set; }
            public string ShopTitle { get; set; }
            public string SizingValueName { get; set; }
            public List<string> Colorfuls { get; set; }
            public List<string> ColorCodes { get; set; }
            public string ShopId { get; set; }
            public long ProductInventoryId { get; set; }
            public int MaxCount { get; set; }
            public string ImageUrl { get; set; }

            public UserProductInventoryPriceModel Costs { get; set; }

            public class UserProductInventoryPriceModel
            {
                public int RowPrice { get; set; }
                public int PriceAdded { get; set; }
                public int Discount { get; set; }
                public int Count { get; set; }
                public int TotalDiscount { get; set; }
                public int TotalPrice { get; set; }
            }
            public UserCartError? CartError { get; set; }
        }

        public class UserProductInventoryPriceAllModel
        {
            public int TotalPriceAll { get; set; }
            public int TotalDiscountAll { get; set; }
            public int CountAll { get; set; }
            public int TotalAmount { get; set; }

        }
    }

    public class UserDetailServiceModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NationalId { get; set; }

        public string RegisterDate { get; set; }

        public string LastLoggedIn { get; set; }

        /// <summary>
        /// آیا کاربر فروشگاه دارد؟
        /// </summary>
        public bool IsSeller { get; set; }

        /// <summary>
        /// نام فروشگاه کاربر
        /// </summary>
        public string ShopTitle { get; set; }

        /// <summary>
        /// لیست تصاویر پروفایل کاربر
        /// </summary>
        public List<string> Avatar { get; set; }

    }

    public class UserFindServiceModel
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// نام کاربر
        /// </summary>

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NationalId { get; set; }

        public List<AvatarServiceModel> Avatars { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>

        public Gender Gender { get; set; }

        public string BirthDate { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public IList<SocialNetworkServiceModel> SocialNetworks { get; set; }

        /// <summary>
        /// نام نمایشی
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// خبرنامه
        /// </summary>
        public bool NewsLetter { get; set; }
        /// <summary>
        /// شماره کارت بانکی
        /// </summary>
        public string BankCardNumber { get; set; }

        public class LocationServiceModel
        {
            /// <summary>
            /// استان
            /// </summary>
            public DefaultServiceModel<long, string> Province { get; set; }
            /// <summary>
            /// شهر
            /// </summary>
            public DefaultServiceModel<long, string> City { get; set; }
            /// <summary>
            /// منطقه
            /// </summary>
            public DefaultServiceModel<long, string> Zone { get; set; }
            /// <summary>
            /// آدرس
            /// </summary>
            public string Address { get; set; }
        }
        public class SocialNetworkServiceModel : DefaultServiceModel<long, string>
        {
            /// <summary>
            /// محتوای رکورد
            /// </summary>
            public string Value { get; set; }

        }
        public class CommunicationServiceModel : DefaultServiceModel<long, string>
        {
            /// <summary>
            /// محتوای رکورد
            /// </summary>
            public string Value { get; set; }

        }

        public class AvatarServiceModel : DefaultServiceModel<string, string>
        {
            /// <summary>
            /// وضعیت تصویر
            /// </summary>
            public Status Status { get; set; }
        }
    }

    public class UserPanelCommentServiceModel
    {
        public List<CommentDetailsServiceServiceModel> ShopComments { get; set; }
        public List<CommentDetailsServiceServiceModel> ProductComments { get; set; }

        public class CommentDetailsServiceServiceModel
        {

        }
    }

    /// <summary>
    /// مدل خروجی پنل کاربر
    /// </summary>
    public class UserPanelServiceModel
    {
        /// <summary>
        /// تاریخ و زمان ثبت نام
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// تاریخ و زمان آخرین بازدید
        /// </summary>
        public DateTime? LastLoggedIn { get; set; }

        /// <summary>
        /// تعداد محصولات مورد علاقه
        /// </summary>
        public int FavoriteProductsCount { get; set; }

        /// <summary>
        /// تعداد فروشگاه های مورد علاقه
        /// </summary>
        public int FavoriteShopsCount { get; set; }

        /// <summary>
        /// تعداد محصولات نشان شده
        /// </summary>
        public int BookmarkedProductsCount { get; set; }

        /// <summary>
        ///  تعداد فروشگاه های دنبال شده
        /// </summary>
        public int FollowedShopsCount { get; set; }

        /// <summary>
        /// تعداد کامنت های کاربر
        /// </summary>
        public int CommentsCount { get; set; }

        /// <summary>
        /// لیست محصولات مورد علاقه
        /// </summary>
        public IList<ProductServiceModel> FavoriteProducts { get; set; }

        /// <summary>
        /// لیست فروشگاه های مورد علاقه
        /// </summary>
        public IList<ShopServiceModel> FavoriteShops { get; set; }

        /// <summary>
        /// لیست محصولات نشان شده
        /// </summary>
        public IList<ProductServiceModel> BookmarkedProducts { get; set; }

        /// <summary>
        /// لیست فروشگاه های دنبال شده
        /// </summary>
        public IList<ShopServiceModel> FollowedShops { get; set; }

        /// <summary>
        /// ده نظر آخر فروشگاه
        /// </summary>
        public IList<ShopCommentServiceModel> ShopComments { get; set; }

        /// <summary>
        /// ده نظر آخر محصول
        /// </summary>
        public IList<ProductCommentServiceModel> ProductComments { get; set; }


        public class ProductServiceModel
        {
            /// <summary>
            /// شناسه محصول
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// نام محصول
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// تصویر محصول
            /// </summary>
            public string Picture { get; set; }
            ///// <summary>
            ///// هزینه ها
            ///// </summary>
            //public ProductCostCalculationServiceModel CostCalculation { get; set; }

            /// <summary>
            /// میزان بازدید
            /// </summary>
            public long Visit { get; set; }
        }

        public class ShopServiceModel
        {
            /// <summary>
            /// شناسه فروشگاه
            /// </summary>
            public string Id { get; set; }
            /// <summary>
            /// نام فروشگاه
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            ///لوگو فروشگاه 
            /// </summary>
            public string Logo { get; set; }

            /// <summary>
            /// تصویر اصلی فروشگاه
            /// </summary>
            public string MainPicture { get; set; }

            /// <summary>
            /// زمینه های کاری فروشگاه
            /// </summary>
            public List<string> Activity { get; set; }

            /// <summary>
            /// میزان بازدید
            /// </summary>
            public long Visit { get; set; }
        }

        public class ShopCommentServiceModel
        {
            /// <summary>
            /// آی دی نظر
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// تاریخ درج | یا دریافت
            /// </summary>
            public DateTime InsertDate { get; set; }

            /// <summary>
            /// عنوان نظر
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// محتوا
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// تعداد پسندیدن
            /// </summary>
            public int Like { get; set; }

            /// <summary>
            /// تعداد نپسندیدن
            /// </summary>
            public int DisLike { get; set; }

            /// <summary>
            /// آی دی والد
            /// </summary>
            public long? ParentId { get; set; }

            /// <summary>
            /// شناسه فروشگاه
            /// </summary>
            public string ShopId { get; set; }

            /// <summary>
            /// عنوان فروشگاه
            /// </summary>
            public string ShopTitle { get; set; }
        }

        public class ProductCommentServiceModel
        {
            /// <summary>
            /// آی دی نظر
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// تاریخ درج | یا دریافت
            /// </summary>
            public DateTime InsertDate { get; set; }

            /// <summary>
            /// عنوان نظر
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// محتوا
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// تعداد پسندیدن
            /// </summary>
            public int? Like { get; set; }

            /// <summary>
            /// تعداد نپسندیدن
            /// </summary>
            public int? DisLike { get; set; }

            /// <summary>
            /// آی دی والد
            /// </summary>
            public long? ParentId { get; set; }

            /// <summary>
            /// شناسه فروشگاه
            /// </summary>
            public long ProductId { get; set; }

            /// <summary>
            /// نام محصول
            /// </summary>
            public string ProductName { get; set; }

        }

    }

    public class UserPanelStatsServiceModel
    {
        public int BookMarking { get; set; }
        public int Comments { get; set; }
        public int Favoriting { get; set; }
        public int Following { get; set; }

    }

    /// <summary>
    /// معرف کاربر پس از لاگین به برنامه
    /// </summary>
    public class UserReagentServiceModel
    {
        /// <summary>
        /// عنوان نمایشی
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// آواتار کاربر
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// آیا نام نمایشی است
        /// </summary>
        public bool IsDisplayName { get; set; }

        /// <summary>
        /// تاریخ ثبت نام
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// تعداد سبد خرید
        /// </summary>
        public int UserCart { get; set; }
        
       

        /// <summary>
        /// شارژ کیف پول
        /// </summary>
        public long WalletCharge { get; set; }

       
    }

    public class UserSearchServiceModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }

    public class UserSocialNetworkEditServiceModel
    {
        [Required]
        public long SocialNetworkId { get; set; }

        [Required]
        public string Link { get; set; }
    }

    public class UserSocialNetworkFindServiceModel
    {
        public long Id { get; set; }
        public long SocialNetworkId { get; set; }
        public string SocialNetworkTitle { get; set; }
        public string Link { get; set; }
        public byte StoreFrom { get; set; }

    }

    /// <summary>
    /// ویو مدل دریافتی شبکه های اجتماعی به همراه مقادیر آنها
    /// </summary>
    public class UserSocialNetworkServiceModel
    {
        public long Id { get; set; }
        public string Value { get; set; }

    }

    public class UserListServiceModel
    {
        [Display(Name = "کد کاربری")]
        [Key]
        public string UserId { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "سطح دسترسی")]
        public List<string> RolesName { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "تلفن همراه")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام نمایشی")]
        public string DisplayName { get; set; }

        [Display(Name = "نام فروشگاه")]
        public string ShopName { get; set; }

        [Display(Name = "آخرین ورود")]
        public DateTime LastLoggedIn { get; set; }
    }

    public class UserAdressServiceModel
    {
        public long AddressId { get; set; }
        public int ProvinceId { get; set; }
        public string Province { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int ZoneId { get; set; }
        public string Zone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Default { get; set; }

    }

    public class UsersInvitedListServiceModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string ShopTitle { get; set; }
        public bool Global { get; set; }
        public DateTime InsertDate { get; set; }
        public string InsertDateP { get; set; }
    }
}


