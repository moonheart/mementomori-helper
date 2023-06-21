using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ミッション機能解放")]
	[MessagePackObject(true)]
	public class MissionOpenContentMB : MasterBookBase
	{
		[Description("ミッションタイプ")]
		[PropertyOrder(1)]
		public MissionAchievementType AchievementType
		{
			get;
		}

		[Description("機能解放")]
		[PropertyOrder(2)]
		public OpenCommandType OpenCommandType
		{
			get;
		}

		[SerializationConstructor]
		public MissionOpenContentMB(long id, bool? isIgnore, string memo, MissionAchievementType achievementType, OpenCommandType openCommandType)
			:base(id, isIgnore, memo)
		{
			AchievementType = achievementType;
			OpenCommandType = openCommandType;
		}

		public MissionOpenContentMB() :base(0L, false, ""){}
	}
}
