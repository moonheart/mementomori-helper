using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class GetBoiPointHistoryListResponse : ApiResponseBase
{
    public List<UserShopPointHistoryInfo> UserShopPointHistoryInfos { get; set; }
}
