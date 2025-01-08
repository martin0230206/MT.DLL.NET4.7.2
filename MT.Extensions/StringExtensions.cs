using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Extensions
{
    /// <summary>
    /// Provides extension methods for string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Attempts to parse a string to an integer.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <returns>The parsed integer, or null if parsing fails.</returns>
        /// <remarks>
        /// This method uses the int.TryParse method to attempt to parse the input string.
        /// If parsing is successful, the method returns the parsed integer. Otherwise, it returns null.
        /// </remarks>
        public static int? TryParseInt(this string input)
        {
            return int.TryParse(input, out int result) ? result : (int?)null;
        }

        /// <summary>
        /// Attempts to parse a string to a DateTime.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <returns>The parsed DateTime, or null if parsing fails.</returns>
        /// <remarks>
        /// This method uses the DateTime.TryParse method to attempt to parse the input string.
        /// If parsing is successful, the method returns the parsed DateTime. Otherwise, it returns null.
        /// </remarks>
        public static DateTime? TryParseDateTime(this string input)
        {
            return DateTime.TryParse(input, out DateTime result) ? result : (DateTime?)null;
        }

        /// <summary>
        /// 擷取字串的前 N 個字，若字串少於 N，則直接返回完整字串。
        /// </summary>
        /// <param name="text">原始字串</param>
        /// <param name="length">要擷取的字數</param>
        /// <param name="addEllipsis">若截斷是否添加省略號</param>
        /// <returns>擷取後的字串</returns>
        public static string Truncate(this string text, int length, bool addEllipsis = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            if (length <= 0)
            {
                return string.Empty; // 若傳入長度為 0 或小於 0，返回空字串
            }

            if (text.Length > length)
            {
                return addEllipsis
                    ? text.Substring(0, length) + "..."
                    : text.Substring(0, length);
            }

            return text;
        }
    }
}
