using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	public class SearchFriendResponse : ApiResponseBase
	{
		public PlayerInfo PlayerInfo { get; set; }
	}
}
