using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class GuildRaidBossInfo
	{
		public string BossGuid { get; set; }

		public string Name { get; set; }

		public long MaxHp { get; set; }

		public long TotalDamage { get; set; }

		public long StartTimeStamp { get; set; }

		public long EndTimeStamp { get; set; }

		public long CurrentHp { get; set; }
	}
}
