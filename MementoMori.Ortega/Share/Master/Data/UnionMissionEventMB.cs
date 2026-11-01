using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("団結ミッションイベントMB")]
	public class UnionMissionEventMB : MasterBookBase, IHasStartEndTime
	{
		[PropertyOrder(1)]
		[Description("ミッション抽選IDのリスト")]
		public IReadOnlyList<long> MissionLotteryIdList
		{
			get;
		}

		[DateTimeString]
		[PropertyOrder(2)]
		[Description("準備期間開始日時")]
		public string ReadyStartTime
		{
			get;
		}

		[DateTimeString]
		[PropertyOrder(3)]
		[Description("イベント開始日時")]
		public string StartTime
		{
			get;
		}

		[DateTimeString]
		[PropertyOrder(4)]
		[Description("イベント終了日時")]
		public string EndTime
		{
			get;
		}

		[DateTimeString]
		[PropertyOrder(5)]
		[Description("バナー終了日時")]
		public string DisplayBannerEndTime
		{
			get;
		}

		[SerializationConstructor]
		public UnionMissionEventMB(long id, bool? isIgnore, string memo, IReadOnlyList<long> missionLotteryIdList, string readyStartTime, string startTime, string endTime, string displayBannerEndTime)
			:base(id, isIgnore, memo)
		{
			MissionLotteryIdList = missionLotteryIdList;
			ReadyStartTime = readyStartTime;
			StartTime = startTime;
			EndTime = endTime;
			DisplayBannerEndTime = displayBannerEndTime;
		}

		public UnionMissionEventMB():base(0, false, "")
		{
		}
	}
}
