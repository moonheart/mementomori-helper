using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild
{
	[MessagePackObject(true)]
	public class GetGuildMemberInfoResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<PlayerInfo> PlayerInfoList { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
