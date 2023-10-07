using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
[OrtegaApi("shop/receiveReward", true, false)]
public class ReceiveRewardRequest : ApiRequestBase
{
    public bool IsBulk { get; set; }

    public long MBId { get; set; }

    public string ProductId { get; set; }

    public long RequiredValue { get; set; }

    public ShopProductType ShopProductType { get; set; }
}