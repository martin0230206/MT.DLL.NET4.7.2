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
    }
}
