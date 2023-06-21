using System.ComponentModel;
using System.Runtime.InteropServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("キャラク動画配信モード")]
	[MessagePackObject(true)]
	public class CharacterLiveModeMB : MasterBookBase
	{
		[Description("終了時間(JP)")]
		[PropertyOrder(4)]
		public float BgmEndTimeJP
		{
			get;
		}

		[Description("終了時間(US)")]
		[PropertyOrder(6)]
		public float BgmEndTimeUS
		{
			get;
		}

		[Description("BGM(JP)")]
		[PropertyOrder(1)]
		public string BgmPathJP
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("BGM(US)")]
		public string BgmPathUS
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("開始時間(JP)")]
		public float BgmStartTimeJP
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("開始時間(US)")]
		public float BgmStartTimeUS
		{
			get;
		}

		[SerializationConstructor]
		public CharacterLiveModeMB(long id, bool? isIgnore, string memo, string bgmPathJP, string bgmPathUS, float bgmStartTimeJP, float bgmEndTimeJP, float bgmStartTimeUS, float bgmEndTimeUS)
			:base(id, isIgnore, memo)
		{
			BgmPathJP = bgmPathJP;
			BgmPathUS = bgmPathUS;
			BgmStartTimeJP = bgmStartTimeJP;
			BgmEndTimeJP = bgmEndTimeJP;
			BgmStartTimeUS = bgmStartTimeUS;
			BgmEndTimeUS = bgmEndTimeUS;
		}

		public CharacterLiveModeMB() : base(0, false, "")
		{
		}

		public void GetBgmData(LanguageType languageType, [Out] string path, [Out] float startTime, [Out] float endTime)
		{
			if (languageType != LanguageType.jaJP)
			{
				string text = this.BgmPathUS;
				return;
			}
			string text2 = this.BgmPathJP;
		}

		public string GetBgmPath(LanguageType languageType)
		{
			if (languageType == LanguageType.jaJP)
			{
				return this.BgmPathJP;
			}
			return this.BgmPathUS;
		}
	}
}
