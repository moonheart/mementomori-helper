using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class DungeonBattleInfo
    {
        public int DungeonBattleVictoryCount { get; set; }

        public bool IsDungeonBattleHardMode { get; set; }

        public int UseDungeonRecoveryItemCount { get; set; }

        public Dictionary<long, int> UseDungeonRelicCountDict { get; set; }

        public DungeonBattleInfo()
        {
        }
    }
}