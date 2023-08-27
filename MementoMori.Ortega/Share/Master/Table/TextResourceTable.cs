using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
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
            if (key.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return _cached.TryGetValue(key, out var value) ? value.Replace("<br>", Environment.NewLine) : key;
        }

        public string Get<T>(T enumValue) where T : Enum
        {
            string name = typeof(T).Name;
            string text2 = enumValue.ToString();
            string text = "[" + name + text2 + "]";
            return this.Get(text);
        }

        public string Get<T>(T enumValue, params object[] param) where T : Enum
        {
            string name = typeof(T).Name;
            string text2 = enumValue.ToString();
            string text = "[" + name + text2 + "]";
            return this.Get(text, param);
        }

        public string Get(string key, params object[] param)
        {
            var value = this.Get(key);
            return string.Format(value, param).Replace("<br>", Environment.NewLine);
        }

        public string GetItemDescription<T>(T enumValue) where T : Enum
        {
            Type type = typeof(T);
            string name = type.Name;
            string text2 = enumValue.ToString();
            string text = "[" + name + text2 + "Description]";
            return this.Get(text);
        }

        public string GetItemDescription<T>(T enumValue, params object[] param) where T : Enum
        {
            Type type = typeof(T);
            string name = type.Name;
            string text2 = enumValue.ToString();
            string text = "[" + name + text2 + "Description]";
            return string.Format(Get(text), param);
        }

        public string GetErrorCodeMessage(ErrorCode errorCode)
        {
            string text = string.Format("[ErrorMessage{0}]", (int)errorCode);
            return this.Get(text);
        }

        public string GetErrorCodeMessage(ErrorCode errorCode, params object[] param)
        {
            string text = string.Format("[ErrorMessage{0}]", (int)errorCode);
            return this.Get(text, param);
        }

        public string GetErrorCodeMessage(long errorCode)
        {
            string text = string.Format("[ErrorMessage{0}]", errorCode);
            return this.Get(text);
        }

        public string GetErrorCodeMessage(long errorCode, params object[] param)
        {
            string text = string.Format("[ErrorMessage{0}]", errorCode);
            return this.Get(text, param);
        }

        public string GetClientErrorCodeMessage(long errorCode)
        {
            string text = string.Format("[ClientErrorMessage{0}]", errorCode);
            return this.Get(text);
        }

        public string GetClientErrorCodeMessage(long errorCode, params object[] param)
        {
            string text = string.Format("[ClientErrorMessage{0}]", errorCode);
            return this.Get(text, param);
        }

        public string GetErrorCodeTitle(ErrorCode errorCode)
        {
            string text = string.Format("[ErrorTitle{0}]", (int)errorCode);
            string text2 = this.Get(text);
            if (!text.Equals(text2))
            {
                return text2;
            }

            return string.Empty;
        }

        public bool IsNullOrEmpty(string key)
        {
            if (_cached != null && !string.IsNullOrEmpty(key))
            {
                return _cached.TryGetValue(key, out var value) && value.IsNullOrEmpty();
            }

            return true;
        }

        public string GetErrorCodeMessage(ClientErrorCode errorCode)
        {
            string text = string.Format("[ClientErrorMessage{0}]", (int)errorCode);
            return this.Get(text);
        }

        public string GetErrorCodeMessage(ClientErrorCode errorCode, params object[] param)
        {
            string text = string.Format("[ClientErrorMessage{0}]", (int)errorCode);
            return this.Get(text, param);
        }

        public string GetClientErrorCodeTitle(ClientErrorCode errorCode)
        {
            string text = string.Format("[ClientErrorTitle{0}]", (int)errorCode);
            string text2 = this.Get(text);
            if (!text.Equals(text2))
            {
                return text2;
            }

            return string.Empty;
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

        private Dictionary<string, string> _cached = new();
        private LanguageType _languageType;
    }
}