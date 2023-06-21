using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class BattleScheduleTable : TableBase<BattleScheduleMB>
	{
		public BattleScheduleMB GetByBattleTypeAndStartTimeFixJST(BattleScheduleType battleScheduleType, DateTime nowJstDateTime)
		{
			// int num = 0;
			// DateTime dateTime;
			// if (dateTime > nowJstDateTime || num != 0)
			// {
			// }
			throw new NullReferenceException();
		}

		public BattleScheduleTable()
		{
		}
	}
}
