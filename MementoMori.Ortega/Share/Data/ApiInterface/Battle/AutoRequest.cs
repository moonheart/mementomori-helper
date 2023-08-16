using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[OrtegaApi("battle/auto", true, false)]
	[MessagePackObject(true)]
	public class AutoRequest : ApiRequestBase
	{
	}
}
