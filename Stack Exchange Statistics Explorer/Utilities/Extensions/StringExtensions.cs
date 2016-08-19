using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string LowercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToLower(s[0]) + s.Substring(1);
        }

        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string IncludeSign(this string s)
        {
            if (s[0] != '-')
            {
                return '+' + s;
            }

            return s;
        }

        public static string AddIth(this string s)
        {
            var check = s.Last().ToString();

            if (s.Length >= 2)
            {
                check = s.Substring(s.Length - 2);
            }

            switch (check)
            {
                case "11":
                case "12":
                case "13":
                    return s + "th";
            }

            switch (check.Last())
            {
                case '1':
                    return s + "st";
                case '2':
                    return s + "nd";
                case '3':
                    return s + "rd";
                default:
                    return s + "th";
            }
        }
    }
}
