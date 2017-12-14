using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4.Tests
{
    public static class Extensions
    {
        public static string ToAssertableString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var pairStrings = dictionary
                .OrderBy(p => p.Key)
                .Select(p => $"{p.Key}: {p.Value}");

            return String.Join("; ", pairStrings);
        }
    }
}