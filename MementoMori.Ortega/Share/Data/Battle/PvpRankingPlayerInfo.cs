using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    public class PvpRankingPlayerInfo
    {
        public long CurrentRank { get; set; }

        public long DefenseBattlePower { get; set; }

        public long BattlePower { get; set; }

        public PlayerInfo PlayerInfo { get; set; }

        public List<UserCharacterInfo> UserCharacterInfoList { get; set; }

        public List<long> GetBattleCharacterIds()
        {
            return UserCharacterInfoList.Select(d => d.CharacterId).ToList();
        }
    }
}