using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	public class BulkApplyFriendsResponse : ApiResponseBase
	{
		public List<long> AppliedPlayerIdList { get; set; }

		public long NewFriendNum { get; set; }
	}
}
