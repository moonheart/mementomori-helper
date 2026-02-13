using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class DungeonBattleGuestTable : TableBase<DungeonBattleGuestMB>
	{
		public List<DungeonBattleGuestMB> GetListByCharacterIds(List<long> characterIds)
		{
			return this._datas.Where(d => characterIds.Contains(d.CharacterId)).ToList();
		}

		public DungeonBattleGuestMB GetByCharacterId(long characterId)
		{
			return this._datas.FirstOrDefault(d => d.CharacterId == characterId);
		}
	}
}
