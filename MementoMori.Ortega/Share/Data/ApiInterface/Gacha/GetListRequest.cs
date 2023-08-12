using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Gacha
{
	[MessagePackObject(true)]
	[OrtegaApi("gacha/getList", true, false)]
	public class GetListRequest : ApiRequestBase
	{
	}
}
