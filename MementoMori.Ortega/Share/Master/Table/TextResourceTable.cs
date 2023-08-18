using System.Runtime.InteropServices;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Interfaces;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class TextResourceTable : ITable
	{
		public bool Load()
		{
			this._cached ??= new Dictionary<string, string>();
			this._cached.Clear();
			var languageType = _languageType;
			if (languageType == LanguageType.None)
			{
				languageType = LanguageType.zhTW;
			}

			var filePath = $"./Master/TextResource{languageType}MB";
			using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				var textResources = this.Deserialize(languageType, fileStream);
				foreach (var textResource in textResources)
				{
					_cached[textResource.StringKey] = textResource.Text;
				}
			}

			return true;
		}

		public string GetMasterBookName()
		{
			return $"TextResource{_languageType}MB";
		}

		public void SetLanguageType(LanguageType languageType)
		{
			this._languageType = languageType;
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
		private static string GetMasterDataPath(LanguageType languageType)
		{
			// int num = 0;
			// TextResourceUtil.TextResourceData[] textResourceDatas = TextResourceUtil.TextResourceDatas;
			// int length = textResourceDatas.Length;
			// if (num < length)
			// {
			// 	TextResourceUtil.TextResourceData textResourceData = textResourceDatas[num];
			// 	if (textResourceData.<LanguageType>k__BackingField != languageType)
			// 	{
			// 		num++;
			// 	}
			// 	string <Name>k__BackingField = textResourceData.<Name>k__BackingField;
			// 	return Application.persistentDataPath + "/Master/" + <Name>k__BackingField;
			// }
			// string empty = string.Empty;
			// return string.Empty;
			throw new NotImplementedException();
		}

		private ITextResource[] Deserialize(LanguageType languageType, Stream data)
		{
			switch (languageType)
			{
				case LanguageType.None: return MessagePackSerializer.Deserialize<TextResourceZhTwMB[]>(data);

				case LanguageType.jaJP: return MessagePackSerializer.Deserialize<TextResourceJaJpMB[]>(data);
				case LanguageType.enUS: return MessagePackSerializer.Deserialize<TextResourceEnUsMB[]>(data);
				case LanguageType.koKR: return MessagePackSerializer.Deserialize<TextResourceKoKrMB[]>(data);
				case LanguageType.zhTW: return MessagePackSerializer.Deserialize<TextResourceZhTwMB[]>(data);
				case LanguageType.frFR: return MessagePackSerializer.Deserialize<TextResourceFrFrMB[]>(data);
				case LanguageType.zhCN: return MessagePackSerializer.Deserialize<TextResourceZhCnMB[]>(data);
				case LanguageType.esMX: return MessagePackSerializer.Deserialize<TextResourceEsMxMB[]>(data);
				case LanguageType.ptBR: return MessagePackSerializer.Deserialize<TextResourcePtBrMB[]>(data);
				case LanguageType.thTH: return MessagePackSerializer.Deserialize<TextResourceThThMB[]>(data);
				case LanguageType.idID: return MessagePackSerializer.Deserialize<TextResourceIdIdMB[]>(data);
				case LanguageType.viVN: return MessagePackSerializer.Deserialize<TextResourceViVnMB[]>(data);
				case LanguageType.ruRU: return MessagePackSerializer.Deserialize<TextResourceRuRuMB[]>(data);
				case LanguageType.deDE: return MessagePackSerializer.Deserialize<TextResourceDeDeMB[]>(data);
				case LanguageType.arEG: return MessagePackSerializer.Deserialize<TextResourceArEgMB[]>(data);
				default: return MessagePackSerializer.Deserialize<TextResourceZhTwMB[]>(data);
			}
		
		}

		private Dictionary<string, string> _cached = new Dictionary<string, string>();
		private LanguageType _languageType;
	}
}
