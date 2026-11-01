using MessagePack;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
[OrtegaApi("shop/getList", true, false)]
public class GetListRequest : ApiRequestBase
{
    public LanguageType LanguageType { get; set; }
}
