using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserBattlePvpDtoInfo
    {
        public long AttackSucceededNum { get; set; }

        public long DefenseSucceededNum { get; set; }

        public long GetTodayDefenseSucceededRewardCount { get; set; }

        public long MaxRanking { get; set; }

        public long PvpLastChallengeTime { get; set; }

        public long PvpTodayCount { get; set; }

        public long PvpTodayUseCurrencyCount { get; set; }

        public UserBattlePvpDtoInfo()
        {
        }
    }
}