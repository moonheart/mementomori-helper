using MementoMori.Ortega.Share.Data.LuckyChance;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LuckyChance;

[MessagePackObject(true)]
[OrtegaApi("luckyChance/registerRealPrizeCItemInfo", true, false)]
public class RegisterRealPrizeCItemInfoRequest : ApiRequestBase
{
    public List<LuckyChanceRealPrizeCItemInfo> RealPrizeCItemInfoList { get; set; }
}
