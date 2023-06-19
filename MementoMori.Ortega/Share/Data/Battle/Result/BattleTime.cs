using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
    [MessagePackObject(true)]
    public class BattleTime
    {
        public long StartBattle { get; set; }

        public long EndBattle { get; }

        public long TotalCommand { get; set; }

        public long TotalCommandOrMinBattleTime { get; set; }

        public BattleTime()
        {
        }
    }
}