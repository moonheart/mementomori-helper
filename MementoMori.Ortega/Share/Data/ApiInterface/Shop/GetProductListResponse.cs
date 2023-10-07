using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class GetProductListResponse : ApiResponseBase
{
    public List<ShopProductSubInfo> ProductSubInfoList { get; set; }

    public List<ShopProductSubInfo> ContractPrivilegeProductSubInfoList { get; set; }
}