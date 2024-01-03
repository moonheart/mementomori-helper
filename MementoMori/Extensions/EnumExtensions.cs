using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Table;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Ortega.Share;

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

        public static string GetName(this GachaRelicType targetRelicType)
        {
            var name = targetRelicType switch
            {
                GachaRelicType.ChaliceOfHeavenly => Masters.TextResourceTable.Get("[ItemName45]"),
                GachaRelicType.SilverOrderOfTheBlueSky => Masters.TextResourceTable.Get("[ItemName46]"),
                GachaRelicType.DivineWingsOfDesire => Masters.TextResourceTable.Get("[ItemName47]"),
                GachaRelicType.FruitOfTheGarden => Masters.TextResourceTable.Get("[ItemName48]"),
                _ => ""
            };
            return name;
        }
    }
}
