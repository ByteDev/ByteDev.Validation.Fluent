using System;
using System.Globalization;

namespace ByteDev.Validation.Fluent
{
    internal static class StringExtensions
    {
        public static bool IsDateTime(this string source, string format)
        {
            return DateTime.TryParseExact(source, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);
        }
    }
}