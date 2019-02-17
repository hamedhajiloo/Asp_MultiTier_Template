namespace Prj.Domain.Enums
{
    /// <summary>
    /// خطا هایی که هنگام تبدیل سبد خرید به سفارش رخ می دهد
    /// </summary>
    public enum UserCartError
    {
        NotError = 0, //بدون خطا
        ProductInventoryNotFound = 1, // موجودی محصول یافت نشد
        ProductInventoryZero = 2, // موجودی محصول به اتمام رسیده
        ProductInventoryMinimum = 3 //موجودی محصول کمتر از تعداد سفارش است
    }
}
