using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("国")]
	public class StateMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("名称キー")]
		public string NameKey
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("国ボーナスID")]
		public long StateBonusId
		{
			get;
		}

		[Description("サブ名称キー")]
		[PropertyOrder(2)]
		public string SubNameKey
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("テキストキー")]
		public string TextKey
		{
			get;
		}

		[Description("クリファテキストキー")]
		[PropertyOrder(4)]
		public string AppearQliphaKey
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("クリファテキストキー")]
		public long AppearQliphaId
		{
			get;
		}

		[Description("開始時間(JP)")]
		[PropertyOrder(7)]
		public float BgmStartTimeJP
		{
			get;
		}

		[Description("開始時間(US)")]
		[PropertyOrder(8)]
		public float BgmStartTimeUS
		{
			get;
		}

		[SerializationConstructor]
		public StateMB(long id, bool? isIgnore, string memo, string nameKey, string subNameKey, string textKey, string appearQliphaKey, long appearQliphaId, long stateBonusId, float bgmStartTimeJP, float bgmStartTimeUS)
			:base(id, isIgnore, memo)
		{
			NameKey = nameKey;
			SubNameKey = subNameKey;
			TextKey = textKey;
			AppearQliphaKey = appearQliphaKey;
			AppearQliphaId = appearQliphaId;
			StateBonusId = stateBonusId;
			BgmStartTimeJP = bgmStartTimeJP;
			BgmStartTimeUS = bgmStartTimeUS;
		}

		public StateMB() :base(0L, false, ""){}

		public float GetBgmStartTime(LanguageType languageType)
		{
			if (languageType == LanguageType.jaJP)
			{
				return this.BgmStartTimeJP;
			}
			return this.BgmStartTimeUS;
		}
	}
}
