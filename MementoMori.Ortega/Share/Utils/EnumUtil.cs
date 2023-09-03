namespace MementoMori.Ortega.Share.Utils
{
	public static class EnumUtil
	{
		public static List<T> GetValueList<T>() where T : Enum
		{
			Type typeFromHandle = typeof(T);
			var values = Enum.GetValues(typeFromHandle);
			return values.Cast<T>().ToList();
		}

		public static T[] GetValueArray<T>() where T : Enum
		{
			Type typeFromHandle = typeof(T);
			var values = Enum.GetValues(typeFromHandle);
			return values.Cast<T>().ToArray();
		}

		public static int GetLength<T>() where T : Enum
		{
			return Enum.GetValues(typeof(T)).Length;
		}

		public static string GetKey<T>(T enumValue) where T : Enum
		{
			string name = typeof(Type).Name;
			string text = enumValue.ToString();
			return "[" + name + text + "]";
		}
	}
}
