using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prj.Domain.Users
{
    /// <summary>
    /// سبد خرید کاربر
    /// </summary>
    [Table("User_Carts")]
    public class UserCart
    {
        public long Id { get; set; }


        [Required]
        [Display(Name = "کاربر")]
        [Index("Group1", IsUnique = true, Order = 1)]
        public string UserId { get; set; } 
        public virtual User User { get; set; }


        //[Display(Name = "موجودی محصول")]
        //[Index("Group1", IsUnique = true, Order = 2)]
        //public long ProductInventoryId { get; set; }
       // public virtual ProductInventory ProductInventory { get; set; }

        [Display(Name = "تعداد")]
        public int Count { get; set; }
    }
}
