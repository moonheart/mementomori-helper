using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidDropEquipmentInfo : ILotteryWeight
	{
		public JobFlags CanEquipJobFlags { get; set; }

		public EquipmentRarityFlags EquipmentRarityFlags { get; set; }

		public EquipmentSlotType EquipmentSlotType { get; set; }

		public int QualityLv { get; set; }

		public int LotteryWeight { get; set; }
	}
}
