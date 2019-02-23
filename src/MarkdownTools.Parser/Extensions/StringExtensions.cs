using System;

namespace MarkdownTools.Parser.Extensions
{
    public static class StringExtensions
    {
        public static string SafeSubstring(this string left, int startIndex)
        {
            if (startIndex >= left.Length)
            {
                return null;
            }

            return left.Substring(startIndex);
        }

        public static string SafeSubstring(this string left, int startIndex, int length)
        {
            if (startIndex >= left.Length)
            {
                return null;
            }

            if (startIndex + length > left.Length)
            {
                length = left.Length - startIndex;
            }

            return left.Substring(startIndex, length);
        }

        public static char SafeGetChar(this string left, int index)
        {
            if (index < left.Length)
            {
                return left[index];
            }

            return '\0';
        }

        public static string NextLine(this string left)
        {
            if (left.Contains(Environment.NewLine))
            {
                return left.SafeSubstring(left.IndexOf(Environment.NewLine, StringComparison.Ordinal) + Environment.NewLine.Length);
            }

            return null;
        }

        public static bool StartsWithExcludingWhitespace(this string left, string right)
        {
            var index = 0;

            while (index < left.Length && (left[index] == ' ' || left[index] == '\t'))
            {
                index++;
            }

            if (index >= left.Length)
            {
                return false;
            }

            return left.SafeSubstring(index).StartsWith(right);
        }
    }
}