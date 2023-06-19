using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[OrtegaApi("battle/getPvpInfo", true, false)]
	[MessagePackObject(true)]
	public class GetPvpInfoRequest : ApiRequestBase
	{
		public GetPvpInfoRequest()
		{
		}
	}
}
