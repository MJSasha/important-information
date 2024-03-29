﻿using ImpInfCommon.Utils.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace ImpInfCommon.Utils
{
    public static class Extentions
    {
        public static string GetRoot(this Type type) => type?
            .GetCustomAttribute<EntityRootAttribute>()?.Root ?? nameof(type);
        public static string GetName(this Enum enumValue) => enumValue.GetType()?
            .GetMember(enumValue.ToString())?
            .First()?
            .GetCustomAttribute<EnumNameAttribute>()?.Name ?? enumValue.ToString();

        public static string ToRusDay(this DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Monday => "Пн",
                DayOfWeek.Tuesday => "Вт",
                DayOfWeek.Wednesday => "Ср",
                DayOfWeek.Thursday => "Чт",
                DayOfWeek.Friday => "Пт",
                DayOfWeek.Saturday => "Сб",
                DayOfWeek.Sunday => "Вс",
                _ => "",
            };
        }
    }
}
