using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TreasureChest
{
	[MessagePackObject(true)]
	public class TreasureChestItemLotteryRate
	{
		public double LotteryRate { get; set; }

		public bool IsCeilingTarget { get; set; }

		public TreasureChestReward TreasureChestReward { get; set; }
	}
}
