using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.GuildRaid;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	public class GetGuildRaidInfoResponse : ApiResponseBase
	{
		public List<GuildRaidInfo> GuildRaidInfos { get; set; }

		public long CanChallengeReleasableBossTimeStamp { get; set; }
	}
}
