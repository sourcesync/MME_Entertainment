using System.Globalization;

namespace WP7Square.Extensions
{
    public static class StringExtensions
    {
        public static string FormatWith(this string value, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, value, args);
        }

        public static long ToLong(this string value, long defaultValue)
        {
            try
            {
                return long.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool TryParse(this string value, out double output)
        {
            try
            {
                output = double.Parse(value);
                return true;
            }
            catch
            {
                output = double.NaN;
            }
            return false;
        }
    }
}
