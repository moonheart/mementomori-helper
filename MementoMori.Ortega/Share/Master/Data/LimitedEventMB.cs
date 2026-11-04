using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;
using MementoMori.Ortega.Share.Data.LimitedEvent;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("期間限定イベント")]
	[MessagePackObject(true)]
	public class LimitedEventMB : MasterBookBase, IHasStartEndTime
	{
		[Description("イベントの種類")]
		[PropertyOrder(1)]
		public LimitedEventType LimitedEventType
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(2)]
		[Description("対象情報リスト")]
		public IReadOnlyList<TargetInfo> TargetInfoList
		{
			get;
		}

		[Description("開始日時")]
		[PropertyOrder(3)]
		public string StartTime
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("終了日時")]
		public string EndTime
		{
			get;
		}

		[SerializationConstructor]
		public LimitedEventMB(long id, bool? isIgnore, string memo, LimitedEventType limitedEventType, string startTime, string endTime, IReadOnlyList<TargetInfo> targetInfoList)
			:base(id, isIgnore, memo)
		{
			LimitedEventType = limitedEventType;
			StartTime = startTime;
			EndTime = endTime;
			TargetInfoList = targetInfoList;
		}

		public LimitedEventMB() :base(0L, false, ""){}

	}
}
