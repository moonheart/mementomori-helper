using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserTowerBattleDtoInfo
    {
        public int BoughtCount { get; set; }

        public long LastUpdateTime { get; set; }

        public long MaxTowerBattleId { get; set; }

        public long PlayerId { get; set; }

        public int TodayBattleCount { get; set; }

        public int TodayBoughtCountByCurrency { get; set; }

        public int TodayClearNewFloorCount { get; set; }

        public TowerType TowerType { get; set; }

        public UserTowerBattleDtoInfo()
        {
        }
    }
}