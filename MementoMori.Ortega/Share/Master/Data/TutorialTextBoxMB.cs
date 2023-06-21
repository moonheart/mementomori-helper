using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("チュートリアルテキストボックス")]
	public class TutorialTextBoxMB : MasterBookBase
	{
		[PropertyOrder(2)]
		[Description("キャラアイコンのキャラID")]
		public long CharacterId
		{
			get;
			set;
		}

		[PropertyOrder(1)]
		[Description("テキストキー")]
		public string TextKey
		{
			get;
			set;
		}

		[PropertyOrder(3)]
		[Description("ボイスファイルパス")]
		public string VoiceJPFilePath
		{
			get;
			set;
		}

		[PropertyOrder(4)]
		[Description("ボイスファイルパス")]
		public string VoiceUSFilePath
		{
			get;
			set;
		}

		[SerializationConstructor]
		public TutorialTextBoxMB(long id, bool? isIgnore, string memo, string textKey, long characterId, string voiceJPFilePath, string voiceUSFilePath)
			:base(id, isIgnore, memo)
		{
			TextKey = textKey;
			CharacterId = characterId;
			VoiceJPFilePath = voiceJPFilePath;
			VoiceUSFilePath = voiceUSFilePath;
		}

		public TutorialTextBoxMB() :base(0L, false, ""){}

		public string GetVoicePath(LanguageType languageType)
		{
			if (languageType != LanguageType.enUS)
			{
				return this.VoiceJPFilePath;
			}
			return this.VoiceUSFilePath;
		}
	}
}
