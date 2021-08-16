using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public static class Extensions
    {
        public static void IsValid(this int number)
        {
            if (number < 0) throw new UserFriendlyError($"Specified value '{number}' is invalid!", 500);
        }

        public static void IsValid(this List<int> numbers)
        {
            numbers.ForEach(x => IsValid(x));
        }

        public static bool IsNotNullOrEmpty<T>(this IList<T> list)
        {
            return list != null && list.Any();
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || !list.Any();
        }

        public static bool IsNull<T>(this T item)
        {
            return item == null;
        }

        public static T ToEnum<T>(this short? value, T defaultValue)
        {
            if (!value.HasValue) return defaultValue;
            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}
