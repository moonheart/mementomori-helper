using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild
{
	[MessagePackObject(true)]
	public class GetGuildIdResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public long GuildId { get; set; }

		public UserSyncData UserSyncData{ get; set; }
	}
}
