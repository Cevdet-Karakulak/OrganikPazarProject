using System.Globalization;

namespace OrganikPazar.Helpers
{
    public static class FormatHelper
    {
        private static readonly CultureInfo TurkishCulture = new CultureInfo("tr-TR");

        public static string FormatCurrency(decimal value)
        {
            if (value >= 1_000_000_000)
                return (value / 1_000_000_000M).ToString("N2", TurkishCulture) + " Milyar ₺";
            if (value >= 1_000_000)
                return (value / 1_000_000M).ToString("N2", TurkishCulture) + " Milyon ₺";
            if (value >= 1_000)
                return (value / 1_000M).ToString("N2", TurkishCulture) + " Bin ₺";

            return value.ToString("C2", TurkishCulture);
        }

        public static string FormatNumber(int value)
        {
            return value.ToString("N0", TurkishCulture);
        }

        public static string SafeCurrencySymbol()
        {
            return "₺";
        }
    }
}
