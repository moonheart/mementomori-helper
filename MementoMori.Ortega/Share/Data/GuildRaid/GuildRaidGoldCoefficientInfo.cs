using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidGoldCoefficientInfo
	{
		public long AdditiveCoefficient { get; set; }

		public long DivisionCoefficient { get; set; }
	}
}
