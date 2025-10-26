using System;
using System.Globalization;

namespace OrganikPazar.Helpers
{
    public static class DateHelper
    {
        private static readonly CultureInfo tr = new("tr-TR");

        private static string Format(DateTime? date, string format)
            => date?.ToString(format, tr) ?? "-";

       public static string ToLongDate(DateTime? date) => Format(date, "d MMMM yyyy");

        public static string ToShortDate(DateTime? date) => Format(date, "dd.MM.yyyy");

        public static string ToDateTime(DateTime? date) => Format(date, "dd.MM.yyyy HH:mm");

        public static string ToRelativeTime(DateTime? date)
        {
            if (date == null)
                return "-";

            var diff = DateTime.Now - date.Value;

            return diff.TotalSeconds switch
            {
                < 60 => "az önce",
                < 3600 => $"{(int)diff.TotalMinutes} dakika önce",
                < 86400 => $"{(int)diff.TotalHours} saat önce",
                < 604800 => $"{(int)diff.TotalDays} gün önce",
                < 2592000 => $"{(int)(diff.TotalDays / 7)} hafta önce",
                < 31536000 => $"{(int)(diff.TotalDays / 30)} ay önce",
                _ => $"{(int)(diff.TotalDays / 365)} yıl önce"
            };
        }
    }
}
