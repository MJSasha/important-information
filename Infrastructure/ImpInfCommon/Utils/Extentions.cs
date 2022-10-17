using System;
using System.Linq;
using System.Reflection;
using TgBotLib.Utils.Attributes;

namespace ImpInfCommon.Utils
{
    public static class Extentions
    {
        public static string GetName(this Enum enumValue) => enumValue.GetType()?
            .GetMember(enumValue.ToString())?
            .First()?
            .GetCustomAttribute<EnumNameAttribute>()?.Name ?? enumValue.ToString();
    }
}
