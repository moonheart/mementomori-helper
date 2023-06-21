using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Time
{
	[MessagePackObject(true)]
	public class StartEndTime : IHasStartEndTime
	{
		[PropertyOrder(2)]
		[Description("終了日時")]
		public string EndTime
		{
			get;
			set;
		}

		[Description("開始日時")]
		[PropertyOrder(1)]
		public string StartTime
		{
			get;
			set;
		}

		public StartEndTime()
		{
		}
	}
}
