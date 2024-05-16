using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	[OrtegaApi("guildTower/getReinforcementJobData", true, false)]
	public class GetReinforcementJobDataRequest : ApiRequestBase
	{
		public Dictionary<JobFlags, int> JobLevelMap { get; set; }
	}
}
