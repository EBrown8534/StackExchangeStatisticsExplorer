using Stack_Exchange_Statistics_Explorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Exchange_Statistics_Explorer.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToUserFriendlyString(this DateTime time, bool localIsUtc, ApplicationUser user, bool includeTime = true, string datePrefix = "", string timePrefix = "at", bool capitalizeModifiers = true)
        {
            return ToUserFriendlyString(time, DateTime.UtcNow, localIsUtc, user, includeTime, datePrefix, timePrefix, capitalizeModifiers);
        }

        public static DateTime ConvertTimeToUtc(this DateTime time)
        {
            var currentDateTime = time;

            if (currentDateTime.Kind != DateTimeKind.Utc)
            {
                return new DateTime(currentDateTime.Ticks, DateTimeKind.Utc);
            }

            return time;
        }

        public static DateTime ConvertUtcToZone(this DateTime utcTime, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        }

        public static string ToUserFriendlyString(this DateTime time, DateTime baseTime, bool localIsUtc, ApplicationUser user, bool includeTime = true, string datePrefix = "", string timePrefix = "at", bool capitalizeModifiers = true)
        {
            if (localIsUtc)
            {
                if (time.Kind != DateTimeKind.Utc)
                {
                    time = time.ConvertTimeToUtc();
                }
                if (baseTime.Kind != DateTimeKind.Utc)
                {
                    baseTime = baseTime.ConvertTimeToUtc();
                }
            }

            if (time.Kind != DateTimeKind.Utc || baseTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"The {nameof(time)} and {nameof(baseTime)} must be of UTC time.");
            }

            string result = string.Empty;

            var localTime = TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.FindSystemTimeZoneById(user?.TimeZoneId ?? "UTC"));
            var localBaseTime = TimeZoneInfo.ConvertTimeFromUtc(baseTime, TimeZoneInfo.FindSystemTimeZoneById(user?.TimeZoneId ?? "UTC"));

            if (!capitalizeModifiers)
            {
                datePrefix = datePrefix.LowercaseFirst();
                timePrefix = timePrefix.LowercaseFirst();
            }

            if (localTime.Date == localBaseTime.Date && (user?.UseRelativeTimes ?? false))
            {
                TimeSpan timeDifference = localBaseTime - localTime;

                if (timeDifference < TimeSpan.FromMinutes(1))
                {
                    result = "Just now";
                }
                else if (timeDifference < TimeSpan.FromMinutes(2))
                {
                    result = "About a minute ago";
                }
                else if (timeDifference < TimeSpan.FromHours(1))
                {
                    result = $"About {Math.Ceiling(timeDifference.TotalMinutes).ToString()} minutes ago";
                }
                else if (timeDifference < TimeSpan.FromHours(2))
                {
                    result = "About an hour ago";
                }
                else
                {
                    result = $"About {Math.Ceiling(timeDifference.TotalHours).ToString()} hours ago";
                }

                if (!capitalizeModifiers)
                {
                    result = result.LowercaseFirst();
                }
            }
            else if (localTime.Date.AddDays(1) == localBaseTime.Date && (user?.UseRelativeTimes ?? false))
            {
                result = "Yesterday";

                if (includeTime)
                {
                    result += $"at {localTime.ToString(ApplicationUser.ValidTimeFormats[user?.TimeFormat ?? 0])}";
                }

                if (!capitalizeModifiers)
                {
                    result = result.LowercaseFirst();
                }
            }
            else if (localTime.Date.AddDays(7) > localBaseTime.Date && (user?.UseRelativeTimes ?? false))
            {
                string timeString = string.Empty;

                if (includeTime)
                {
                    timeString = $" {timePrefix} {localTime.ToString(ApplicationUser.ValidTimeFormats[user?.TimeFormat ?? 0])}";
                }

                result = $"{datePrefix}{localTime.DayOfWeek.ToString()}{timeString}";
            }
            else
            {
                string timeString = string.Empty;

                if (includeTime)
                {
                    timeString = $" {timePrefix} {localTime.ToString(ApplicationUser.ValidTimeFormats[user?.TimeFormat ?? 0])}";
                }

                result = $"{datePrefix}{localTime.ToString(ApplicationUser.ValidDateFormats[user?.DateFormat ?? 0])}{timeString}";
            }

            return result;
        }
    }
}
