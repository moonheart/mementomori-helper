using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserBattleLegendLeagueDtoInfo
    {
        public long AttackSucceededNum { get; set; }

        public long DefenseSucceededNum { get; set; }

        public long LegendLeagueLastChallengeTime { get; set; }

        public long LegendLeagueTodayCount { get; set; }

        public long LegendLeagueTodayUseCurrencyCount { get; set; }

        public int LegendLeagueConsecutiveVictoryCount { get; set; }

        public UserBattleLegendLeagueDtoInfo()
        {
        }
    }
}