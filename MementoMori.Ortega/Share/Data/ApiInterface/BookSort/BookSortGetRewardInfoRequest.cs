using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSort/getRewardInfo", true, false)]
	public class BookSortGetRewardInfoRequest : ApiRequestBase
	{
	}
}
