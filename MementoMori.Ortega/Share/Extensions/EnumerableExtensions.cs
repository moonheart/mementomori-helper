using System.Runtime.InteropServices;

namespace MementoMori.Ortega.Share.Extensions
{
	public static class EnumerableExtensions
	{
		// public static bool Any<T>(this List A_0)
		// {
		// 	if (A_0 == 0)
		// 	{
		// 	}
		// 	bool flag;
		// 	return flag;
		// }
		//
		// public static IEnumerable Distinct<TSource, TResult>(this IEnumerable A_0, Func A_1)
		// {
		// 	throw new NullReferenceException();
		// }
		//
		// public static IEnumerable<IEnumerable<T>> Divide<T>(this IEnumerable<T> source, int count)
		// {
		// 	throw new NullReferenceException();
		// }
		//
		// public static IEnumerable<TResult> FindDuplication<TResult, TSource>(this IEnumerable<TSource> enumerable, Func<TSource, TResult> selector)
		// {
		// 	throw new NullReferenceException();
		// }
		//
		// public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> enumerable, int count)
		// {
		// 	throw new NullReferenceException();
		// }
		//
		// public static IEnumerable<ValueTuple<T, int>> Indexed<T>(this IEnumerable<T> source)
		// {
		// 	throw new NullReferenceException();
		// }

		public static bool IsNullOrEmpty<T>(this string a0)
		{
			return string.IsNullOrEmpty(a0);
		}
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> a0)
		{
			return a0 == null || !a0.Any();
		}

		public static bool IsNullOrEmpty<T>(this List<T> a0)
		{
			return a0 == null || !a0.Any();

		}

		// public static IEnumerable<TSource> MaxAll<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, TResult> selector) where TResult : IEquatable<TResult>
		// {
		// 	throw new NullReferenceException();
		// }
		//
		// public static void MoveToTop<T>(this List A_0, int A_1)
		// {
		// 	IEnumerator enumerator = A_0.GetEnumerator();
		// }
		//
		// public static IEnumerable<T> Padding<T>(this IEnumerable<T> enumerable, int count, [Optional] T value)
		// {
		// 	throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
		// }
		
		public static Task WhenAll(this IEnumerable<Task> tasks)
		{
			return Task.WhenAll(tasks);
		}
		
		public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks)
		{
			return Task.WhenAll(tasks);
		}
	}
}
