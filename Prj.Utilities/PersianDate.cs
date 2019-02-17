using System;
using System.Linq;
using System.Globalization;

namespace Prj.Utilities
{
    public static class PersianDate
    {
        //public static DateTime ToGregorian(this DateTime dateTime)
        //{
        //    PersianCalendar persianCalender = new PersianCalendar();

        //    return persianCalender.ToDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, 0);
        //}

        //public static PersianDateTime ToPersianDateTime(this DateTime dateTime)
        //{
        //    return new PersianDateTime(dateTime);
        //}

        //public static PersianDateTime ToPersianDateTime(this string dateTime)
        //{
        //    return new PersianDateTime(Convert.ToDateTime(dateTime));
        //}

        /// <summary>
        /// نمایش کامل تاریخ با عنوان روزهای هفته
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToPersianDayOfWeekString(DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(DateTime.Now).ToString();
            string month = persianCalendar.GetMonth(DateTime.Now).ToString();
            string dayofmonth = persianCalendar.GetDayOfMonth(DateTime.Now).ToString();
            string week = persianCalendar.GetDayOfWeek(DateTime.Now).ToString();
            switch (week)
            {
                case "Saturday":
                    week = "شنبه";
                    break;
                case "Sunday":
                    week = "یکشنبه";
                    break;
                case "Monday":
                    week = "دوشنبه";
                    break;
                case "Tuesday":
                    week = "سه شنبه";
                    break;
                case "Wednesday":
                    week = "چهارشنبه";
                    break;
                case "Thursday":
                    week = "پنج شنبه";
                    break;
                case "Friday":
                    week = "جمعه";
                    break;
            }
            switch (month)
            {
                case "1":
                    month = "فروردین";
                    break;
                case "2":
                    month = "اردیبهشت";
                    break;
                case "3":
                    month = "خرداد";
                    break;
                case "4":
                    month = "تیر";
                    break;
                case "5":
                    month = "مرداد";
                    break;
                case "6":
                    month = "شهریور";
                    break;
                case "7":
                    month = "مهر";
                    break;
                case "8":
                    month = "آبان";
                    break;
                case "9":
                    month = "آذر";
                    break;
                case "10":
                    month = "دی";
                    break;
                case "11":
                    month = "بهمن";
                    break;
                case "12":
                    month = "اسفند";
                    break;
            }
            string today = week + " " + dayofmonth + " " + month + " " + year;
            return today;
        }

        /// <summary>
        /// نمایش کامل تاریخ به شمسی
        /// </summary>
        /// <param name="dt">تاریخ</param>
        /// <param name="longstring">نمایش ساعت ؟</param>
        /// <returns></returns>
        /// <summary>
        /// نمایش کامل تاریخ به شمسی
        /// </summary>
        /// <param name="dt">تاریخ</param>
        /// <param name="longstring">نمایش ساعت ؟</param>
        /// <returns></returns>
        public static string ToPersianDateString(DateTime dt,
            bool includeHourMinute = false,
            bool rtlTemplate = false,
            string separator = "/",
            bool includeSecond = false)
        {

            var year = dt.Year;
            var month = dt.Month;
            var day = dt.Day;
            var persianCalendar = new PersianCalendar();
            var pYear = persianCalendar.GetYear(new DateTime(year, month, day, new GregorianCalendar()));
            var pMonth = persianCalendar.GetMonth(new DateTime(year, month, day, new GregorianCalendar()));
            var pDay = persianCalendar.GetDayOfMonth(new DateTime(year, month, day, new GregorianCalendar()));

            var dateTime = $"{pYear}{separator}{pMonth.ToString("00", CultureInfo.InvariantCulture)}{separator}{pDay.ToString("00", CultureInfo.InvariantCulture)}";


            if (includeHourMinute)
            {
                var time = $"{dt.Hour.ToString("00")}:{dt.Minute.ToString("00")}";

                if (includeSecond)
                    time += $":{dt.Second.ToString("00")}";

                //برای قالب‌هایی که راست به چپ هستند درست نمایش داده می‌شود
                if (rtlTemplate)
                    dateTime = time + " " + dateTime;
                else
                    dateTime += " " + time;

            }

            return dateTime;
        }

        public static DateTime PersianDateToGregorianDate(string pDate)
        {
            var dateParts = pDate.Split(new[] { '/' }).Select(d => int.Parse(d)).ToArray();
            var hour = 0;
            var min = 0;
            var seconds = 0;
            return new DateTime(dateParts[0], dateParts[1], dateParts[2],
                                hour, min, seconds, new PersianCalendar());
        }
    }
}