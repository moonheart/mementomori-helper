using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Pvp
{
    // Character lists (AttackCharacterInfoList/DefenseCharacterInfoList) left out: unused, MessagePack skips them.
    [MessagePackObject(true)]
    public class PvpBattleLogInfo
    {
        public long AttackBattlePower { get; set; }
        public BattleEndInfo BattleEndInfo { get; set; }
        public long BattleTime { get; set; }
        public string BattleToken { get; set; }
        public long DefenseBattlePower { get; set; }
        public long NewRank { get; set; }
        public long OldRank { get; set; }
        public PlayerInfo RivalPlayerInfo { get; set; }
    }
}
