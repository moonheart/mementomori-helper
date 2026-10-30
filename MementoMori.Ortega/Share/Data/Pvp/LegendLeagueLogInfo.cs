using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Pvp
{
    // Character lists (Attack/DefenseCharacterDtoInfoList) left out: unused, MessagePack skips them.
    [MessagePackObject(true)]
    public class LegendLeagueLogInfo
    {
        public long AttackBattlePower { get; set; }
        public BattleEndInfo BattleEndInfo { get; set; }
        public long BattleTime { get; set; }
        public string BattleToken { get; set; }
        public long DefenseBattlePower { get; set; }
        public long GetPoint { get; set; }
        public PlayerInfo RivalPlayerInfo { get; set; }
        public int TargetConsecutiveVictoryCount { get; set; }
        public long TargetPoint { get; set; }
    }
}
