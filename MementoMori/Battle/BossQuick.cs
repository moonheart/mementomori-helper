using MementoMori.Ortega.Share.Data;

namespace MementoMori.Battle;

public class BossQuick
{
    public class Req
    {
        public int QuestId { get; set; }
    }

    public class Resp
    {
        public BattleRewardResult BattleRewardResult { get; set; }
        public UserSyncData UserSyncData { get; set; }
    }

    public class BattleRewardResult
    {
        public int CharacterExp { get; set; }
        public DropItemList[] DropItemList { get; set; }
        public int ExtraGold { get; set; }
        public FixedItemList[] FixedItemList { get; set; }
        public int PlayerExp { get; set; }
        public int RankUp { get; set; }
    }

    public class DropItemList
    {
        public int ItemCount { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
    }

    public class FixedItemList
    {
        public int ItemCount { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
    }


}