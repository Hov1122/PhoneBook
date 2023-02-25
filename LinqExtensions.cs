using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBookConsole
{
    // modify OrderBy and ThenBy such that they can sort in ascending or descending order based on boolean parameter
    public static class LinqExtensions
    {
        public static IOrderedEnumerable<TSource> OrderByFlag<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool descending = false)
        {
            return descending
                ? source.OrderByDescending(keySelector)
                : source.OrderBy(keySelector);
        }

        public static IOrderedEnumerable<TSource> ThenByFlag<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool descending = false)
        {
            return descending
                ? source.ThenByDescending(keySelector)
                : source.ThenBy(keySelector);
        }
    }
}
