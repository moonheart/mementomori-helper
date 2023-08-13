using System.Collections;

namespace MementoMori.Ortega.Share.Extensions
{
	public static class DictionaryExtension
	{
		public static TValue GetOrAdd<TValue, TKey>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> createFn)
		{
			if (dict.TryGetValue(key, out var value))
			{
				return value;
			}

			dict[key] = createFn(key);
			return dict[key];
		}

		public static TValue GetOrDefault<TValue, TKey>(this IDictionary A_0, TKey key)
		{
			if (A_0.Contains(key))
			{
				return (TValue) A_0[key];
			}

			return default;
		}

		public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> dict1, IDictionary<TKey, TValue> dict2)
		{
			if (dict1 == null)
			{
				return;
			}
			foreach (var key in dict2.Keys)
			{
				dict1[key] = dict2[key];
			}
		}
	}
}
