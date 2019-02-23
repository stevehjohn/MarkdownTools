using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkdownTools.Parser.Extensions
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var index = 0;

            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public static IEnumerable<T> GetRange<T>(this IEnumerable<T> enumerable, int start, int length)
        {
            var list = enumerable.ToList();

            var range = new List<T>();

            var index = 0;

            while (index < length)
            {
                range.Add(list[start + index]);
                index++;
            }

            return range;
        }

        public static IEnumerable<T> RemoveRange<T>(this IEnumerable<T> enumerable, int start, int length)
        {
            var list = enumerable.ToList();

            var result = new List<T>();

            for (var i = 0; i < list.Count; i++)
            {
                if (i < start || i >= start + length)
                {
                    result.Add(list[i]);
                }
            }

            return result;
        }
    }
}