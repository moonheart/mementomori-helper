using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.StarsGuidanceTradeShop
{
    [MessagePackObject(true)]
    public class StarsGuidanceConsumeItemInfo
    {
        public ItemType ConsumeItemType { get; set; }
        public long ConsumeItemId { get; set; }
        public long ConsumeItemCount { get; set; }
        public long GiveItemCount { get; set; }
    }
}
