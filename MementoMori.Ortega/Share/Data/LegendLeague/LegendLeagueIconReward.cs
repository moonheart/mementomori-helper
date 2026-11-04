using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LegendLeague
{
    [MessagePackObject(true)]
    public class LegendLeagueIconReward
    {
        public long Id { get; set; }
        public long StartLocalTime { get; set; }
        public long EndLocalTime { get; set; }
        public long ConsumeItemCount { get; set; }
        public UserItem GiveItem { get; set; }
        public LegendLeagueIconRewardLimitTradeType LimitTradeType { get; set; }
        public int TradeCount { get; set; }
        public int LimitTradeCount { get; set; }
        public int Order { get; set; }
        public LegendLeagueIconRewardDisplayTabType DisplayTabType { get; set; }
        public bool IsNew { get; set; }
    }
}
