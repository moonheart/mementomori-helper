using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BountyQuest
{
	[MessagePackObject(true)]
	[OrtegaApi("bountyQuest/getList", true, false)]
	public class GetListRequest : ApiRequestBase
	{
		public GetListRequest()
		{
		}
	}
}
