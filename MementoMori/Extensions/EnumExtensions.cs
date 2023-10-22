using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MementoMori.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDesc<TEnum>(this TEnum value) where TEnum: Enum
        {
            var fieldInfo = typeof(TEnum).GetField(value.ToString());
            if (fieldInfo != null)
            {
                return fieldInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;
            }

            return "";
        }
    }
}
