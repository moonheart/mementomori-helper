using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("団結ミッションMB")]
	public class UnionMissionMB : MasterBookBase, IHasStartEndTime
	{
		[PropertyOrder(1)]
		[Description("ミッション説明")]
		public string DescriptionKey
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("ミッション達成難易度")]
		public int Difficulty
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("ミッション達成タイプ")]
		public MissionAchievementType AchievementType
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("達成要求値")]
		public long RequireValue
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("獲得ポイント")]
		public long GetPoint
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("達成期限")]
		public int LimitDays
		{
			get;
		}

		[PropertyOrder(9)]
		[Description("チケットで選択可能か")]
		public bool IsSelectableByTicket
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("開始日時")]
		public string StartTime
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("終了日時")]
		public string EndTime
		{
			get;
		}

		[SerializationConstructor]
		public UnionMissionMB(long id, bool? isIgnore, string memo, string descriptionKey, int difficulty, MissionAchievementType achievementType, long requireValue, long getPoint, int limitDays, string startTime, string endTime, bool isSelectableByTicket)
			:base(id, isIgnore, memo)
		{
			DescriptionKey = descriptionKey;
			Difficulty = difficulty;
			AchievementType = achievementType;
			RequireValue = requireValue;
			GetPoint = getPoint;
			LimitDays = limitDays;
			StartTime = startTime;
			EndTime = endTime;
			IsSelectableByTicket = isSelectableByTicket;
		}

		public UnionMissionMB():base(0, false, "")
		{
		}
	}
}
