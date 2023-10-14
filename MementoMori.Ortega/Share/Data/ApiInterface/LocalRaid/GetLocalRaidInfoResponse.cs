using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid
{
	[MessagePackObject(true)]
	public class GetLocalRaidInfoResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<long> OpenLocalRaidQuestIds { get; set; }

		public List<long> OpenEventLocalRaidQuestIds { get; set; }
		public Dictionary<long, int> ClearCountDict { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
