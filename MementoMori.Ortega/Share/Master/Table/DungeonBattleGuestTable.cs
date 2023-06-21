using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class DungeonBattleGuestTable : TableBase<DungeonBattleGuestMB>
	{
		public List<DungeonBattleGuestMB> GetListByCharacterIds(List<long> characterIds)
		{
			// List<DungeonBattleGuestMB> list = new List();
			// int num = 0;
			// bool flag;
			// if (flag)
			// {
			// }
			// num++;
			// return list;
			throw new NotImplementedException();
		}

		public DungeonBattleGuestMB GetByCharacterId(long characterId)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public DungeonBattleGuestTable()
		{
		}
	}
}
