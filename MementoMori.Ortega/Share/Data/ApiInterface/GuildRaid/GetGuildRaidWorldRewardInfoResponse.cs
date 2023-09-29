using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.GuildRaid;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	public class GetGuildRaidWorldRewardInfoResponse : ApiResponseBase
	{
		public long TotalWorldDamage { get; set; }

		public List<WorldRewardInfo> WorldRewardInfos{ get; set; }
	}
}
