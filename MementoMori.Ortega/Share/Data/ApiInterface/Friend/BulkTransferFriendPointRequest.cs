using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/bulkTransferFriendPoint", true, false)]
	public class BulkTransferFriendPointRequest : ApiRequestBase
	{
		public BulkTransferFriendPointRequest()
		{
		}
	}
}
