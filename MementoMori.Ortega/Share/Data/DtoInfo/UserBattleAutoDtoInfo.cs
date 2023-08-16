using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserBattleAutoDtoInfo
    {
        public long AverageBattleTime { get; set; }

        public int BattleEfficiency { get; set; }

        public int ConsecutiveWinCount { get; set; }

        public long CurrentQuestId { get; set; }

        public long CurrentMaxQuestId { get; set; }

        public long ExpectedCharacterExp { get; set; }

        public long ExpectedPlayerExp { get; set; }

        public long QuickLastExecuteTime { get; set; }

        public long QuickTodayUseCurrencyCount { get; set; }

        public long QuickTodayUsePrivilegeCount { get; set; }

        public BattleResult BattleResult { get; set; }

        public UserBattleAutoDtoInfo()
        {
            // long DefaultAverageTime = OrtegaConst.BattleAuto.DefaultAverageTime;
            // this.AverageBattleTime = DefaultAverageTime;
            // int DefaultEfficiency = OrtegaConst.BattleAuto.DefaultEfficiency;
            // this.BattleEfficiency = DefaultEfficiency;
            // long DefaultQuestId = OrtegaConst.BattleAuto.DefaultQuestId;
            // this.CurrentQuestId = DefaultQuestId;
            // long DefaultQuestId2 = OrtegaConst.BattleAuto.DefaultQuestId;
            // this.CurrentMaxQuestId = DefaultQuestId2;
            // base..ctor();
        }
    }
}