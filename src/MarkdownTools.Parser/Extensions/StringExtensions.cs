﻿namespace MarkdownTools.Parser.Extensions
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
            if (startIndex > -left.Length || startIndex + length >= left.Length)
            {
                return null;
            }

            return left.Substring(startIndex, length);
        }
    }
}