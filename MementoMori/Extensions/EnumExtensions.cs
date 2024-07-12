using System.ComponentModel;
using System.Reflection;

namespace MementoMori.Extensions;

public static class EnumExtensions
{
    public static string GetDesc<TEnum>(this TEnum value) where TEnum : Enum
    {
        var fieldInfo = typeof(TEnum).GetField(value.ToString());
        if (fieldInfo != null) return fieldInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;

        return "";
    }

    public static string GetName(this GachaRelicType targetRelicType)
    {
        var name = targetRelicType switch
        {
            GachaRelicType.ChaliceOfHeavenly => TextResourceTable.Get("[ItemName45]"),
            GachaRelicType.SilverOrderOfTheBlueSky => TextResourceTable.Get("[ItemName46]"),
            GachaRelicType.DivineWingsOfDesire => TextResourceTable.Get("[ItemName47]"),
            GachaRelicType.FruitOfTheGarden => TextResourceTable.Get("[ItemName48]"),
            _ => ""
        };
        return name;
    }
}