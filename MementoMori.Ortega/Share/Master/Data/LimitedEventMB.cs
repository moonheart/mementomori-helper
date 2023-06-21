using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

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

		[Description("開始日時")]
		[PropertyOrder(2)]
		public string StartTime
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("終了日時")]
		public string EndTime
		{
			get;
		}

		[SerializationConstructor]
		public LimitedEventMB(long id, bool? isIgnore, string memo, LimitedEventType limitedEventType, string startTime, string endTime)
			:base(id, isIgnore, memo)
		{
			LimitedEventType = limitedEventType;
			StartTime = startTime;
			EndTime = endTime;
		}

		public LimitedEventMB() :base(0L, false, ""){}
	}
}
