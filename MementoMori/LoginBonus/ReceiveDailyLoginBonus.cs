using MementoMori.Ortega.Share.Data;

namespace MementoMori.LoginBonus;

public class ReceiveDailyLoginBonus
{
    public class Req
    {
        public int ReceiveDay { get; set; }
    }

    public class Resp
    {
        public RewardItemList[]? RewardItemList { get; set; }
        public UserSyncData? UserSyncData { get; set; }
    }

    public class RewardItemList
    {
        public int ItemCount { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
    }

}