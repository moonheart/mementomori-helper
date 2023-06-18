using MementoMori.Ortega.Share.Data;

namespace MementoMori.Present;

public class ReceiveItem
{
    public class Req
    {
        public int LanguageType { get; set; }
        public object PresentGuid { get; set; }
    }

    public class Resp
    {
        public ResultItems[]? ResultItems { get; set; }
        public UpsertPresentDtoInfoList[]? UpsertPresentDtoInfoList { get; set; }
        public UserSyncData? UserSyncData { get; set; }
    }

    public class ResultItems
    {
        public Item Item { get; set; }
        public int RarityFlags { get; set; }
    }

    public class Item
    {
        public int ItemCount { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
    }

    public class UpsertPresentDtoInfoList
    {
        public long CreateAt { get; set; }
        public long DisplayLimitDate { get; set; }
        public string Guid { get; set; }
        public bool IsReceived { get; set; }
        public ItemList[] ItemList { get; set; }
        public string Message { get; set; }
        public long ReceiveLimitDate { get; set; }
        public string Title { get; set; }
    }

    public class ItemList
    {
        public Item1 Item { get; set; }
        public int RarityFlags { get; set; }
    }

    public class Item1
    {
        public int ItemCount { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
    }
}