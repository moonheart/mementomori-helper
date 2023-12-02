using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class TowerBattleQuestTable : TableBase<TowerBattleQuestMB>
	{
		public TowerBattleQuestMB GetByTowerTypeAndFloor(long floor, TowerType towerType)
        {
            return _datas.FirstOrDefault(d => d.Floor == floor && d.TowerType == towerType);
        }

		public long GetMaxFloor(TowerType towerType)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public TowerBattleQuestTable()
		{
		}
	}
}
