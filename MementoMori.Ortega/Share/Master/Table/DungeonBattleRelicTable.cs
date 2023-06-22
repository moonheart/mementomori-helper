using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
    public class DungeonBattleRelicTable : TableBase<DungeonBattleRelicMB>
    {
        public DungeonBattleRelicMB GetByReinforceFrom(long reinforceFrom)
        {
            return _datas.FirstOrDefault(d => d.ReinforceFrom == reinforceFrom);
        }

        public List<DungeonBattleRelicMB> GetByRelicIds(List<long> relicIds)
        {
            return _datas.Where(d => relicIds.Contains(d.Id)).ToList();
        }

        public DungeonBattleRelicTable()
        {
        }
    }
}