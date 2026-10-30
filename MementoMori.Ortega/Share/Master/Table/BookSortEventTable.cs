using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class BookSortEventTable : TableBase<BookSortEventMB>
	{
		public BookSortEventMB GetByInTime(OrtegaTimeManager timeManager)
		{
			if (timeManager == null)
			{
				throw new ArgumentNullException(nameof(timeManager));
			}

			return timeManager.GetInTimeData(_datas) as BookSortEventMB;
		}
	}
}
