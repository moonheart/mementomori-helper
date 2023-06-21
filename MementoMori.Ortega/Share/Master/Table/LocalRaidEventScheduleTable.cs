using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class LocalRaidEventScheduleTable : TableBase<LocalRaidEventScheduleMB>
	{
		public LocalRaidEventScheduleMB GetByNowDateTime(DateTime nowDateTime)
		{
			// DateTime dateTime;
			// DateTime dateTime2;
			// if (!(dateTime <= nowDateTime) || !(nowDateTime <= dateTime2))
			// {
			// }
			throw new NullReferenceException();
		}

		public LocalRaidEventScheduleTable()
		{
		}
	}
}
