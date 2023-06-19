using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Auth
{
    [MessagePackObject(true)]
    public class PlayerDataInfo
    {
        public long CharacterId { get; set; }

        public long LastLoginTime { get; set; }

        public LegendLeagueClassType LegendLeagueClass { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public long PlayerId { get; set; }

        public long PlayerRank { get; set; }

        public long WorldId { get; set; }

        public PlayerDataInfo()
        {
        }
    }
}