using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/searchFriend", true, false)]
	public class SearchFriendRequest : ApiRequestBase
	{
		public long SearchPlayerId { get; set; }
	}
}
