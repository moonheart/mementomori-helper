using MessagePack;

namespace MementoMori.Ortega.Share.Data.LocalRaid
{
	[MessagePackObject(true)]
	public class LocalRaidStartEndTime
	{
		public int StartTime
		{
			get;
			set;
		}

		public int EndTime
		{
			get;
			set;
		}

		public LocalRaidStartEndTime()
		{
		}
	}
}
