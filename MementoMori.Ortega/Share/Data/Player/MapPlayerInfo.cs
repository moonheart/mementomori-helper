using MessagePack;

namespace MementoMori.Ortega.Share.Data.Player
{
    [MessagePackObject(true)]
    public class MapPlayerInfo
    {
        public long LatestQuestId { get; set; }

        public long MainCharacterIconId { get; set; }

        public long PlayerId { get; set; }

        public long PlayerRank { get; set; }

        public long QuestId { get; set; }

        public MapPlayerInfo()
        {
        }
    }
}