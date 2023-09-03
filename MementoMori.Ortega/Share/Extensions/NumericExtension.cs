using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Extensions
{
	public static class NumericExtension
	{
		public static string ToStringSeparateFourDigitsWithSpace(this long value)
		{
			return value.ToString("#### #### #### ####");
		}

		public static string ToStringWithUnit(this long value, LanguageType languageType)
		{
			// if (value < 100000)
			// {
			// 	return value.ToString();
			// }
			//
			// switch (languageType)
			// {
			// 	case LanguageType.jaJP:
			// 	case LanguageType.koKR:
			// 	case LanguageType.zhTW:
			// 	case LanguageType.zhCN:
			// 		
			// }
			//
			// if (languageType - LanguageType.jaJP <= 13)
			// {
			// 	int num = 0;
			// 	int num2 = 0;
			// 	double num3 = Math.Pow((double)num, 6.3706613826E-314);
			// 	int num4 = (int)(languageType + 1);
			// 	if (num4 != 0)
			// 	{
			// 		if (num4 < 0)
			// 		{
			// 		}
			// 		double num5 = Math.Truncate((double)num2);
			// 		double num6 = Math.Truncate((double)num2);
			// 	}
			// 	double num7 = Math.Truncate((double)num2);
			// 	string text;
			// 	return string.Format("{0}{1}", text, text);
			// }
			// return string.Empty;
			throw new NotImplementedException();
		}

		public static string ToStringWithUnitEnUS(this long value, LanguageType languageType)
		{
			// int num = 0;
			// int num2 = (int)(languageType + (int)languageType);
			// int num3 = num2;
			// num2 += num3;
			// num2 += num2;
			// double num4 = Math.Truncate((double)num);
			// double num5 = Math.Truncate((double)num);
			// double num6 = Math.Truncate((double)num);
			// ulong num7;
			// string text;
			// return string.Format("{0}{1}", num7, text);
			throw new NotImplementedException();
		}

		private static string ToStringWithUnitAsia(long value, LanguageType languageType)
		{
			// int num = 0;
			// int num2 = (int)(languageType + 1);
			// if (num2 != 0)
			// {
			// 	if (num2 < 0)
			// 	{
			// 	}
			// 	double num3 = Math.Truncate((double)num);
			// 	double num4 = Math.Truncate((double)num);
			// }
			// double num5 = Math.Truncate((double)num);
			// string text;
			// return string.Format("{0}{1}", text, text);
			throw new NotImplementedException();

		}

		private static string GetUnitString(LanguageType languageType, int unitId)
		{
			if (languageType - LanguageType.jaJP <= 13)
			{
				return OrtegaConst.Common.NumberUnitJaJP[unitId];
			}
			return string.Empty;
		}

		public static string ToStringWithComma(this int value)
		{
			return value.ToString("N0").FixSeparatorsForLocator();
		}

		public static string ToStringWithComma(this double value)
		{
			return value.ToString("N0").FixSeparatorsForLocator();
		}

		public static string ToStringWithComma(this long value)
		{
			return value.ToString("N0").FixSeparatorsForLocator();
		}

		public static string ToStringDataSize(this long bytes)
		{
			// float num = Math.Max((float)(0 * (int)9.536743E-07f), 0.01f);
			// string text = string.Format("{0:F2}MB", "{0:F2}MB");
			// float num2 = (float)(0 * (int)9.313226E-10f);
			// return string.Format("{0:F2}GB", text).FixSeparatorsForLocator();
			throw new NotImplementedException();
		}

		public static string FixSeparatorsForLocator(this string value)
		{
			return value;
			// GameManager instance = SingletonMonoBehaviour.Instance;
			// LanguageType <CurrentLanguageType>k__BackingField = instance.<CurrentLanguageType>k__BackingField;
			// if (instance != (ulong)4294967293L && <CurrentLanguageType>k__BackingField != LanguageType.deDE)
			// {
			// 	return value;
			// }
			// string text;
			// return text;
		}
	}
}
