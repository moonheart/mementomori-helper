using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DungeonBattle
{
    [MessagePackObject(true)]
    public class DungeonBattleGrid
    {
        public string DungeonGridGuid { get; set; }

        public long DungeonGridId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public DungeonBattleGrid()
        {
        }
    }
}