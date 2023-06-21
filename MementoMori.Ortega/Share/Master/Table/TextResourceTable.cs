using System.Runtime.InteropServices;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class TextResourceTable : TableBase<TextResourceMB>
	{
		public bool Load(LanguageType languageType)
		{
			base.Load();
			foreach (var textResourceMb in _datas)
			{
				switch (languageType)
				{
					case LanguageType.None:
						break;
					case LanguageType.jaJP:
						_cached[textResourceMb.StringKey] = textResourceMb.jaJP;
						break;
					case LanguageType.enUS:
						_cached[textResourceMb.StringKey] = textResourceMb.enUS;
						break;
					case LanguageType.koKR:
						_cached[textResourceMb.StringKey] = textResourceMb.koKR;
						break;
					case LanguageType.zhTW:
						_cached[textResourceMb.StringKey] = textResourceMb.zhTW;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(languageType), languageType, null);
				}
			}

			return true;
			// TextResourceMB textResourceMB;
			// string[] array3;
			// for (;;)
			// {
			// 	int num = 0;
			// 	if (this._cached == num)
			// 	{
			// 		Dictionary<string, string> dictionary = new Dictionary();
			// 		this._cached = dictionary;
			// 	}
			// 	this._cached.Clear();
			// 	string name = typeof(TextResourceMB).Name;
			// 	string masterDataPath = TableBase.GetMasterDataPath(name);
			// 	if (!File.Exists(masterDataPath))
			// 	{
			// 		goto IL_2AB;
			// 	}
			// 	FileStream fileStream;
			// 	byte[] array = new byte[fileStream.Length];
			// 	int length = array.Length;
			// 	int num2 = 0;
			// 	int num3 = fileStream.Read(array, num2, length);
			// 	TextResourceMB[] array2;
			// 	if (num < array2.Length)
			// 	{
			// 		textResourceMB = array2[num];
			// 		if (!string.IsNullOrEmpty(textResourceMB.StringKey))
			// 		{
			// 			Dictionary<string, string> cached = this._cached;
			// 			string StringKey = textResourceMB.StringKey;
			// 			if (!cached.ContainsKey(StringKey))
			// 			{
			// 				int num4 = languageType - LanguageType.jaJP;
			// 				if (num4 != 0)
			// 				{
			// 					if (num4 != 0)
			// 					{
			// 						if (num4 != 0)
			// 						{
			// 							if (num4 != 1)
			// 							{
			// 								goto IL_1A9;
			// 							}
			// 							Dictionary<string, string> cached2 = this._cached;
			// 							string zhTW = textResourceMB.zhTW;
			// 							string StringKey2 = textResourceMB.StringKey;
			// 							cached2.Add(StringKey2, zhTW);
			// 							num++;
			// 						}
			// 						Dictionary<string, string> cached3 = this._cached;
			// 						string koKR = textResourceMB.koKR;
			// 						string StringKey3 = textResourceMB.StringKey;
			// 						cached3.Add(StringKey3, koKR);
			// 						num++;
			// 					}
			// 					Dictionary<string, string> cached4 = this._cached;
			// 					string enUS = textResourceMB.enUS;
			// 					string StringKey4 = textResourceMB.StringKey;
			// 					cached4.Add(StringKey4, enUS);
			// 					num++;
			// 				}
			// 				Dictionary<string, string> cached5 = this._cached;
			// 				string jaJP = textResourceMB.jaJP;
			// 				string StringKey5 = textResourceMB.StringKey;
			// 				cached5.Add(StringKey5, jaJP);
			// 				num++;
			// 			}
			// 			string text = textResourceMB.StringKey + "が重複しています。";
			// 			num++;
			// 			goto IL_196;
			// 		}
			// 		goto IL_196;
			// 		IL_1A9:
			// 		num++;
			// 		goto IL_1AD;
			// 		IL_196:
			// 		string text2 = textResourceMB.StringKey + "が設定されていません。";
			// 		goto IL_1A9;
			// 	}
			// 	IL_1AD:
			// 	string text3 = "TableBase cache loaded. MB:" + name + " Path:" + masterDataPath;
			// 	int num5 = 0;
			// 	int num6 = 0;
			// 	text3.FieldGetter(num6, num5, masterDataPath);
			// 	if ("{il2cpp array field local14->}" != (ulong)0L)
			// 	{
			// 	}
			// 	if (num != 0)
			// 	{
			// 		goto IL_306;
			// 	}
			// 	array3 = new string[6];
			// 	if ("TableBase file not exists. MB:" != 0 && "TableBase file not exists. MB:" == 0)
			// 	{
			// 		goto IL_306;
			// 	}
			// 	array3[0] = "TableBase file not exists. MB:";
			// 	if (name == 0 || "TableBase file not exists. MB:" != 0)
			// 	{
			// 		array3[1] = name;
			// 		if (" Path:" == 0 || " Path:" != 0)
			// 		{
			// 			array3[2] = " Path:";
			// 			if (masterDataPath == 0 || " Path:" != 0)
			// 			{
			// 				array3[3] = masterDataPath;
			// 				if (" " == 0 || " " != 0)
			// 				{
			// 					array3[4] = " ";
			// 					if (" " == 0 || " " != 0)
			// 					{
			// 						break;
			// 					}
			// 				}
			// 			}
			// 		}
			// 	}
			// }
			// array3[5] = " ";
			// string text4 = string.Concat(array3);
			// IL_2AB:
			// int num7 = 0;
			// string text5;
			// text5.FieldGetter(num7, " Path:", textResourceMB);
			// throw new NullReferenceException();
			// IL_306:
			// throw new NullReferenceException();
			// throw new NotImplementedException();
		}

		public string Get(string key)
		{
			return _cached.TryGetValue(key, out var value) ? value : "未知";
			// if (this._cached != (ulong)0L)
			// {
			// 	if (string.IsNullOrEmpty(key))
			// 	{
			// 		return string.Empty;
			// 	}
			// 	Dictionary<string, string> cached = this._cached;
			// 	bool flag;
			// 	bool flag2;
			// 	if (flag && !flag2)
			// 	{
			// 		string newLine = Environment.NewLine;
			// 		string text;
			// 		return text;
			// 	}
			// }
			// return key;
			throw new NotImplementedException();
		}

		public string Get<T>(T enumValue) where T : Enum
		{
			// string name = typeof(Type).Name;
			// string text2;
			// string text = "[" + name + text2 + "]";
			// return this.Get(text);
			throw new NotImplementedException();
		}

		public string Get<T>(T enumValue, params object[] param) where T : Enum
		{
			// Type type;
			// string name = type.Name;
			// string text2;
			// string text = "[" + name + text2 + "]";
			// return this.Get(text, param);
			throw new NotImplementedException();
		}

		public string Get(string key, params object[] param)
		{
			// string text2;
			// string[] array;
			// for (;;)
			// {
			// 	string text = string.Format(this.Get(key), param);
			// 	string newLine = Environment.NewLine;
			// 	text2 = text.Replace("<br>", newLine);
			// 	array = new string[5];
			// 	if ("invalid Parameters (" == 0 || "invalid Parameters (" != 0)
			// 	{
			// 		array[0] = "invalid Parameters (";
			// 		if ("invalid Parameters (" != 0 && (")(" == 0 || ")(" != 0))
			// 		{
			// 			array[2] = ")(";
			// 			if (")(" == 0 || ")(" != 0)
			// 			{
			// 				array[3] = ")(";
			// 				if (")" == 0 || ")" != 0)
			// 				{
			// 					break;
			// 				}
			// 			}
			// 		}
			// 	}
			// }
			// array[4] = ")";
			// string text3 = string.Concat(array);
			// return text2;
			throw new NotImplementedException();
		}

		public string GetItemDescription<T>(T enumValue) where T : Enum
		{
			// Type type;
			// string name = type.Name;
			// string text2;
			// string text = "[" + name + text2 + "Description]";
			// return this.Get(text);
			throw new NotImplementedException();
		}

		public string GetItemDescription<T>(T enumValue, params object[] param) where T : Enum
		{
			// string name = typeof(Type).Name;
			// string text2;
			// string text = "[" + name + text2 + "Description]";
			// return this.Get(text, param);
			throw new NotImplementedException();
		}

		public string GetErrorCodeMessage(ErrorCode errorCode)
		{
			string text = string.Format("[ErrorMessage{0}]", "[ErrorMessage{0}]");
			return this.Get(text);
		}

		public string GetErrorCodeMessage(ErrorCode errorCode, params object[] param)
		{
			string text = string.Format("[ErrorMessage{0}]", "[ErrorMessage{0}]");
			return this.Get(text, param);
		}

		public string GetErrorCodeMessage(long errorCode)
		{
			string text = string.Format("[ErrorMessage{0}]", "[ErrorMessage{0}]");
			return this.Get(text);
		}

		public string GetErrorCodeMessage(long errorCode, params object[] param)
		{
			string text = string.Format("[ErrorMessage{0}]", "[ErrorMessage{0}]");
			return this.Get(text, param);
		}

		public string GetClientErrorCodeMessage(long errorCode)
		{
			string text = string.Format("[ClientErrorMessage{0}]", "[ClientErrorMessage{0}]");
			return this.Get(text);
		}

		public string GetClientErrorCodeMessage(long errorCode, params object[] param)
		{
			string text = string.Format("[ClientErrorMessage{0}]", "[ClientErrorMessage{0}]");
			return this.Get(text, param);
		}

		public string GetErrorCodeTitle(ErrorCode errorCode)
		{
			string text = string.Format("[ErrorTitle{0}]", "[ErrorTitle{0}]");
			string text2 = this.Get(text);
			if (!text.Equals(text2))
			{
				return text2;
			}
			return string.Empty;
		}

		public bool IsNullOrEmpty(string key)
		{
			// if (this._cached != (ulong)0L && !string.IsNullOrEmpty(key))
			// {
			// 	Dictionary<string, string> cached = this._cached;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		bool flag2;
			// 		return flag2;
			// 	}
			// }
			// return true;
			throw new NotImplementedException();
		}

		public string GetErrorCodeMessage(ClientErrorCode errorCode)
		{
			string text = string.Format("[ClientErrorMessage{0}]", "[ClientErrorMessage{0}]");
			return this.Get(text);
		}

		public string GetErrorCodeMessage(ClientErrorCode errorCode, params object[] param)
		{
			string text = string.Format("[ClientErrorMessage{0}]", "[ClientErrorMessage{0}]");
			return this.Get(text, param);
		}

		public string GetClientErrorCodeTitle(ClientErrorCode errorCode)
		{
			string text = string.Format("[ClientErrorTitle{0}]", "[ClientErrorTitle{0}]");
			string text2 = this.Get(text);
			if (!text.Equals(text2))
			{
				return text2;
			}
			return string.Empty;
		}

		public TextResourceTable()
		{
		}

		// [FieldOffset(Offset = "0x18")]
		private Dictionary<string, string> _cached = new Dictionary<string, string>();
	}
}
