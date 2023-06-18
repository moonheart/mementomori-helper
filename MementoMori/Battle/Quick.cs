using MementoMori.Ortega.Share.Data;

namespace MementoMori.Battle;

public class Quick
{
    public class Req
    {
        public int QuestQuickExecuteType { get; set; }
        public int QuickCount { get; set; }
    }

    public class Resp
    {
        public AutoBattleRewardResult AutoBattleRewardResult { get; set; }
        public long QuickLastExecuteTime { get; set; }
        public int QuickTodayUseCurrencyCount { get; set; }
        public int QuickTodayUsePrivilegeCount { get; set; }
        public UserSyncData UserSyncData { get; set; }
    }

    public class AutoBattleRewardResult
    {
        public int BattleCountAll { get; set; }
        public int BattleCountWin { get; set; }
        public BattleRewardResult BattleRewardResult { get; set; }
        public int BattleTotalTime { get; set; }
        public int GoldByPopulation { get; set; }
        public int PotentialJewelByPopulation { get; set; }
    }

    public class BattleRewardResult
    {
        public int CharacterExp { get; set; }
        public DropItemList[] DropItemList { get; set; }
        public int ExtraGold { get; set; }
        public object[] FixedItemList { get; set; }
        public int PlayerExp { get; set; }
        public int RankUp { get; set; }
    }

    public class DropItemList
    {
        public int ItemCount { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
    }

}