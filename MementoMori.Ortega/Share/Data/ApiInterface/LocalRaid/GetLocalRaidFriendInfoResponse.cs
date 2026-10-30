using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid
{
	[MessagePackObject(true)]
	public class GetLocalRaidFriendInfoResponse : ApiResponseBase
	{
		public Dictionary<long, int> PlayerChallengeCountDict { get; set; }

		public List<PlayerInfo> PlayerInfoList { get; set; }
	}
}
