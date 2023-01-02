using System;

namespace ImpInfCommon.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumNameAttribute : Attribute
    {
        public string Name { get; set; }

        public EnumNameAttribute(string name)
        {
            Name = name;
        }
    }
}
