using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidDamageBar
	{
		public int DamageBarCount
		{
			get;
			set;
		}

		public long DamageBarMaxValue
		{
			get;
			set;
		}

		public GuildRaidDamageBar()
		{
		}
	}
}
