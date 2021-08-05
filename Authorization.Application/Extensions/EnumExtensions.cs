using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Authorization.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var enumString = enumValue.ToString();
            var enumMember = enumType.GetMember(enumString).First();
            var displayNameAttribute = enumMember.GetCustomAttribute<DisplayAttribute>();

            return displayNameAttribute == null ? enumString : displayNameAttribute.Name;
        }
    }
}
