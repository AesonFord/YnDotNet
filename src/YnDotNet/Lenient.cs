using System.Collections.Generic;

namespace YnDotNet
{
    /// <summary>
    /// Provides lenient string matching for yes/no values.
    /// </summary>
    internal static class Lenient
    {
        private const double YesMatchScoreThreshold = 2.0;
        private const double NoMatchScoreThreshold = 1.25;

        private static readonly Dictionary<char, double> YMatch = new Dictionary<char, double>
        {
            { '5', 0.25 },
            { '6', 0.25 },
            { '7', 0.25 },
            { 't', 0.75 },
            { 'y', 1.0 },
            { 'u', 0.75 },
            { 'g', 0.25 },
            { 'h', 0.25 },
            { 'j', 0.25 }
        };

        private static readonly Dictionary<char, double> EMatch = new Dictionary<char, double>
        {
            { '2', 0.25 },
            { '3', 0.25 },
            { '4', 0.25 },
            { 'w', 0.75 },
            { 'e', 1.0 },
            { 'r', 0.75 },
            { 's', 0.25 },
            { 'd', 0.25 },
            { 'f', 0.25 }
        };

        private static readonly Dictionary<char, double> SMatch = new Dictionary<char, double>
        {
            { 'q', 0.25 },
            { 'w', 0.25 },
            { 'e', 0.25 },
            { 'a', 0.75 },
            { 's', 1.0 },
            { 'd', 0.75 },
            { 'z', 0.25 },
            { 'x', 0.25 },
            { 'c', 0.25 }
        };

        private static readonly Dictionary<char, double> NMatch = new Dictionary<char, double>
        {
            { 'h', 0.25 },
            { 'j', 0.25 },
            { 'k', 0.25 },
            { 'b', 0.75 },
            { 'n', 1.0 },
            { 'm', 0.75 }
        };

        private static readonly Dictionary<char, double> OMatch = new Dictionary<char, double>
        {
            { '9', 0.25 },
            { 'a', 0.25} ,
            { '0', 0.25 },
            { 'i', 0.75 },
            { 'o', 1.0 },
            { 'p', 0.75 },
            { 'k', 0.25 },
            { 'l', 0.25 }
        };

        /// <summary>
        /// Calculates a score for how closely the input matches "yes".
        /// </summary>
        private static double GetYesMatchScore(string value)
        {
            double score = 0;

            if (value.Length >= 1 && YMatch.TryGetValue(char.ToLower(value[0]), out double yScore))
            {
                score += yScore;
            }

            if (value.Length >= 2 && EMatch.TryGetValue(char.ToLower(value[1]), out double eScore))
            {
                score += eScore;
            }

            if (value.Length >= 3 && SMatch.TryGetValue(char.ToLower(value[2]), out double sScore))
            {
                score += sScore;
            }

            return score;
        }

        /// <summary>
        /// Calculates a score for how closely the input matches "no".
        /// </summary>
        private static double GetNoMatchScore(string value)
        {
            double score = 0;

            if (value.Length >= 1 && NMatch.TryGetValue(char.ToLower(value[0]), out double nScore))
            {
                score += nScore;
            }

            if (value.Length >= 2 && OMatch.TryGetValue(char.ToLower(value[1]), out double oScore))
            {
                score += oScore;
            }

            return score;
        }

        /// <summary>
        /// Performs lenient parsing of yes/no values.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        /// <param name="defaultValue">The default value to return if parsing fails.</param>
        /// <returns>Boolean value based on lenient parsing or the default value if parsing fails.</returns>
        public static bool? Parse(string input, bool? defaultValue)
        {
            if (GetYesMatchScore(input) >= YesMatchScoreThreshold)
            {
                return true;
            }

            if (GetNoMatchScore(input) >= NoMatchScoreThreshold)
            {
                return false;
            }

            return defaultValue;
        }
    }
}