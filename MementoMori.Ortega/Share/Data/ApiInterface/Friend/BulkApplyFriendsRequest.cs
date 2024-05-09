using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/bulkApplyFriends", true, false)]
	public class BulkApplyFriendsRequest : ApiRequestBase
	{
		public List<long> TargetPlayerIdList { get; set; }
	}
}
