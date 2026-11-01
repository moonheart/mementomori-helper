using MessagePack;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop
{
	[MessagePackObject(true)]
	[OrtegaApi("shop/registerActionLog", true, false)]
	public class RegisterActionLogRequest : ApiRequestBase
	{
		public WebStoreGuidanceType WebStoreGuidanceType { get; set; }
	}
}
