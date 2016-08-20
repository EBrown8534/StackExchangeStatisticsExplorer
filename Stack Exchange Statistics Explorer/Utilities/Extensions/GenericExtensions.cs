using Evbpc.Framework.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.Utilities.Extensions
{
    public static class GenericExtensions
    {
        public static string GetClassOption<T>(this T value, Predicate<T> good, Predicate<T> bad, Predicate<T> neutral, Predicate<T> grey) =>
            new Dictionary<string, Predicate<T>>
            {
                ["good"] = good,
                ["bad"] = bad,
                ["neutral"] = neutral,
                ["grey"] = grey
            }.FindKey(value);

        public static string GetClassOption<T>(this T value, Predicate<T> good, Predicate<T> bad, Predicate<T> neutral) =>
            value.GetClassOption(good, bad, neutral, x => true);

        public static string GetClassOption<T>(this T value, Predicate<T> good, Predicate<T> bad) =>
            value.GetClassOption(good, bad, x => true);
    }
}
