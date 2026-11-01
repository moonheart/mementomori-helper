using MementoMori.Ortega.Share.Data.LuckyChance;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LuckyChance;

[MessagePackObject(true)]
public class LuckyChanceGetDetailInfoResponse : ApiResponseBase
{
    public List<LuckyChanceLotteryItemInfo> LotteryItemList { get; set; }
}
