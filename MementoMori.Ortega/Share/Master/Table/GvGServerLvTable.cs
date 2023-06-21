using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class GvGServerLvTable : TableBase<GvGServerLvMB>
	{
		public GvGServerLvMB GetGvGServerLvMBByWorldMB(long localTimestamp, long worldCreateTimestamp)
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)0);
			int num = 0;
			if (num != 0)
			{
			}
			num++;
			throw new NullReferenceException();
		}

		public GvGServerLvMB GetGvGServerLvMBByWorldGroupMB(WorldGroupMB worldGroupMB, Dictionary<long, long> worldCreateTimeDict, long localTimestamp)
		{
			throw new NullReferenceException();
		}

		public GvGServerLvTable()
		{
		}
	}
}
