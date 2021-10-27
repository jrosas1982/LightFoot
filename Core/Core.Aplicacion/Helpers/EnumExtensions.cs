using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Core.Aplicacion.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null) { return string.Empty; }
            if (!Enum.IsDefined(enumValue.GetType(), enumValue)) { return string.Empty; }
            var customAtt = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();

            if (customAtt is null)
                return null;

            return customAtt.GetName();
        }

        public static string GetGroupName(this Enum enumValue)
        {
            if (enumValue == null) { return string.Empty; }
            if (!Enum.IsDefined(enumValue.GetType(), enumValue)) { return string.Empty; }
            var customAtt = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();

            if (customAtt is null)
                return null;

            return customAtt.GetGroupName();
        }

        public static List<T> GetFlagList<T>(this T enumValue) where T : Enum
        {
            if (!enumValue.IsFlagDefined()) return null;

            return Enum.GetValues(typeof(T))
                        .Cast<T>()
                        .Where(x => enumValue.HasFlag(x) && !x.Equals(0))
                        .ToList();
        }

        public static List<T> GetFlagList<T>(int enumValue) where T : struct, Enum
        {
            //if (Enum.TryParse(enumValue.ToString(), out enumConverted))
            T enumConverted = Enum.Parse<T>(enumValue.ToString());

            if (!enumConverted.IsFlagDefined()) return null;

            return Enum.GetValues(typeof(T))
                        .Cast<T>()
                        .Where(x => enumConverted.HasFlag(x))
                        .ToList();
        }

        public static bool IsFlagDefined(this Enum value)
        {
            dynamic dyn = value;
            var max = Enum.GetValues(value.GetType()).Cast<dynamic>().Aggregate((e1, e2) => e1 | e2);
            return (max & dyn) == dyn;
        }

        //public static T GenerateFlag<T>(this IEnumerable<T> enumIEnumerable) where T : Enum
        //{
        //    var result = enumIEnumerable.Cast<dynamic>().Aggregate((accum, enumValue) => accum | enumValue);

        //    return result;
        //}

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IEnumerable<T> GetValues<T>(this T elEnum) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
