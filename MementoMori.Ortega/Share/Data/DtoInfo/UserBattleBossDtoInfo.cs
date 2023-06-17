using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserBattleBossDtoInfo
    {
        public long BossLastChallengeTime { get; set; }

        public long BossClearMaxQuestId { get; set; }

        public long BossTodayUseCurrencyCount { get; set; }

        public long BossTodayUseTicketCount { get; set; }

        public long BossTodayWinCount { get; set; }

        public bool IsOpenedNewQuest { get; set; }

        public UserBattleBossDtoInfo()
        {
        }
    }
}