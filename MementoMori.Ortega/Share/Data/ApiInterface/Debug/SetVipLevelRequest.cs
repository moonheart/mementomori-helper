using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug
{
	[MessagePackObject(true)]
	[OrtegaApi("debug/setVipLevel", true, false)]
	public class SetVipLevelRequest : ApiRequestBase
	{
		public long VipLevel { get; set; }
	}
}
