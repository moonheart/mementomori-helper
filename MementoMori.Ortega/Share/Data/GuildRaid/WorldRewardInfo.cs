using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class WorldRewardInfo
	{
		public long GoalDamage { get; set; }

		public bool IsReceived { get; set; }
	}
}
