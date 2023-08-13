using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.User
{
	[MessagePackObject(true)]
	[OrtegaApi("user/getMypage", true, false)]
	public class GetMypageRequest : ApiRequestBase
	{
	}
}
