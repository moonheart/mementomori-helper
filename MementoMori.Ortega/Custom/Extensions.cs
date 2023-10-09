using System.Collections;
using System.Drawing;

namespace MementoMori.Ortega.Custom;

public static class Extensions
{
    public static List<T> Merge<T>(this List<T> list1, List<T> list2, Func<T, T, bool> comparer = null)
    {
        if (list1 == null)
        {
            return list2;
        }

        if (list2 == null)
        {
            return list1;
        }
        
        foreach (var item in list2)
        {
            if (list1.Exists(x=>comparer?.Invoke(x, item) ?? x.Equals(item)))
            {
                list1.RemoveAll(x => comparer?.Invoke(x, item) ?? x.Equals(item));
            }
            list1.Add(item);
        }

        return list1;
    }

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2)
    {
        if (dict1 == null)
        {
            return dict2;
        }

        if (dict2 == null)
        {
            return dict1;
        }

        foreach (var value in dict2)
        { 
            dict1[value.Key] = value.Value;
        }

        return dict1;
    }

    public static bool IsNotNullOrEmpty<T>(this IReadOnlyList<T> list)
    {
        return list != null && list.Count > 0;
    }

    public static bool IsNotNullOrEmpty(this IDictionary dict)
    {
        return dict != null && dict.Count > 0;
    }

    public static long DefaultIfInvalid(this long n) => n >= 0 ? n : 0;
    public static int DefaultIfInvalid(this int n) => n >= 0 ? n : 0;

    // public static Color ToColor(this uint number)
    // {
    //     var a = number >> 24;
    //     var r = number >> 16 & 0xFF;
    //     var g = number >> 8 & 0xFF;
    //     var b = number & 0xFF;
    //     return Color.FromArgb(a, r, g, b);
    // }
}