using System;

namespace Prj.Utilities
{
    public static class DateTimeExtentions
    {
        const int Second = 1;
        const int Minute = 60 * Second;
        const int Hour = 60 * Minute;
        const int Day = 24 * Hour;
        const int Month = 30 * Day;

        public static string GetPersianPastTimeString(this DateTime dateTime)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
            var delta = Math.Abs(ts.TotalSeconds);
            if (delta < 1 * Minute)
            {
                return "لحظاتی قبل"; //ts.Seconds <= 10 ? "لحظاتی پیش" : ts.Seconds + " ثانیه پیش";
            }
            if (delta < 2 * Minute)
            {
                return "یک دقیقه قبل";
            }
            if (delta < 45 * Minute)
            {
                return ts.Minutes + " دقیقه قبل";
            }
            if (delta < 90 * Minute)
            {
                return "یک ساعت قبل";
            }
            if (delta < 24 * Hour)
            {
                return ts.Hours + " ساعت قبل";
            }
            if (delta < 48 * Hour)
            {
                return "دیروز";
            }
            if (delta < 30 * Day)
            {
                return ts.Days + " روز پیش";
            }
            if (delta < 12 * Month)
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "یک ماه پیش" : months + " ماه پیش";
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "یک سال پیش" : years + " سال پیش";

        }
    }
}
