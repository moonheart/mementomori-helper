using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class QuestTable : TableBase<QuestMB>
	{
		public List<QuestMB> GetListByChapterId(long chapterId)
		{
			// List<QuestMB> list = new List();
			// int num = 0;
			// num++;
			// return list;
			throw new NullReferenceException();
		}

		public QuestMB GetByQuestMapBuildingId(long questMapBuildingId)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public QuestMB GetLastQuestMBByChapterId(long chapterId)
		{
			throw new NullReferenceException();
		}

		public QuestTable()
		{
		}
	}
}
