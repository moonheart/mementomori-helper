using MementoMori.Ortega.Share.Data;

namespace MementoMori.Vip;

public class GetDailyGift
{
    public class Req
    {
    }

    public class Resp
    {
        public ItemList[]? ItemList { get; set; }
        public UserSyncData UserSyncData { get; set; }
    }

    public class ItemList
    {
        public int ItemCount { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
    }

}