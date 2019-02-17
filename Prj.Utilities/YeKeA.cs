namespace Prj.Utilities
{
    public static class YeKeA
    {
        public const char ArabicYeChar = (char)1610;
        public const char PersianYeChar = (char)1740;

        public const char ArabicKeChar = (char)1603;
        public const char PersianKeChar = (char)1705;

        public const char PersianABaKolah = (char)1570;
        public const char PersianABiKolah = (char)1575;

        /// <summary>
        /// تبدیل ك ي آ به ی ک ا به همراه تریم
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ApplyCorrectYeKeA(this string data)
        {
            return string.IsNullOrWhiteSpace(data) ?
                        string.Empty :
                        data.Replace(ArabicYeChar, PersianYeChar)
                            .Replace(ArabicKeChar, PersianKeChar)
                            .Replace(PersianABaKolah, PersianABiKolah)
                            .Trim();
        }

        /// <summary>
        /// تبدیل ك ي آ به ی ک ا بدون تریم
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ApplyCorrectYeKeAWitOutTrim(this string data)
        {
            return string.IsNullOrWhiteSpace(data) ?
                        string.Empty :
                        data.Replace(ArabicYeChar, PersianYeChar)
                            .Replace(ArabicKeChar, PersianKeChar);
        }

        /// <summary>
        /// تبدیل ك ي به ی ک به همراه تریم
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ApplyCorrectYeKe(this string data)
        {
            return string.IsNullOrWhiteSpace(data) ?
                        string.Empty :
                        data.Replace(ArabicYeChar, PersianYeChar)
                            .Replace(ArabicKeChar, PersianKeChar)
                            .Trim();
        }

        /// <summary>
        /// تبدیل ك ي به ی ک بدون تریم
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ApplyCorrectYeKeWitOutTrim(this string data)
        {
            return string.IsNullOrWhiteSpace(data)
                ? string.Empty
                : data.Replace(ArabicYeChar, PersianYeChar);
        }
    }
}
