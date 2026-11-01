using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug
{
	[MessagePackObject(true)]
	[OrtegaApi("debug/unionMissionResetAcceptCount", true, false)]
	public class UnionMissionResetAcceptCountRequest : ApiRequestBase
	{
	}
}
