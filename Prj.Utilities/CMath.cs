namespace Prj.Utilities
{
    public static class CMath
    {
        /// <summary>
        /// رند کردن عدد به حالت دسیمال
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static decimal HalfRounded(decimal number)
        {
            //عدد بصورت خشک و بدون اعشار
            var intNumber = (int)number;

            //جمع عدد بدون اعشار با نیم
            var decimalNumber = (decimal)((intNumber + 0.5));

            //آیا عدد اصلی از عدد دسیمال نامبر بزرگتر یا مساوی است
            return number >= decimalNumber ? decimalNumber : intNumber;
        }
    }
}
