using System.Text.RegularExpressions;

namespace YnDotNet
{
    /// <summary>
    /// Parse yes/no like values - C# implementation of the 'yn' library.
    /// </summary>
    public static class Yn
    {
        /// <summary>
        /// Parse yes/no like values.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="lenient">Whether to use lenient parsing.</param>
        /// <param name="defaultValue">The default value if parsing fails.</param>
        /// <returns>Boolean value or the default value if parsing fails.</returns>
        public static bool? Parse(object value, bool lenient = false, bool? defaultValue = null)
        {
            if (value == null)
            {
                return defaultValue;
            }

            string stringValue = value.ToString().Trim();

            if (Regex.IsMatch(stringValue, "^(?:y|yes|true|1|on|enabled)$", RegexOptions.IgnoreCase))
            {
                return true;
            }

            if (Regex.IsMatch(stringValue, "^(?:n|no|false|0|off|disabled)$", RegexOptions.IgnoreCase))
            {
                return false;
            }

            if (lenient)
            {
                return Lenient.Parse(stringValue, defaultValue);
            }

            return defaultValue;
        }
    }
}