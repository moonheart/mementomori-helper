using MessagePack;

namespace MementoMori.Ortega.Share.Data.LuckyChance
{
	[MessagePackObject(true)]
	public class LuckyChanceRealPrizeCItemInfo
	{
		public long ItemId { get; set; }

		public long CharacterId { get; set; }
	}
}
