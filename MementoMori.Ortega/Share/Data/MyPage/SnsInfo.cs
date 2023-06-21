using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MyPage
{
	[MessagePackObject(true)]
	[Description("SNS情報")]
	public class SnsInfo
	{
		[Description("SNS名称のキー")]
		public string NameKey { get; set; }

		[Description("URL")]
		public string Url { get; set; }

		[Description("ミッション達成条件タイプ")]
		public MissionAchievementType MissionAchievementType
		{
			get;
			set;
		}

		public SnsInfo()
		{
		}
	}
}
