using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TreasureChest
{
	[MessagePackObject(true)]
	public class TreasureChestItemLotteryRateListInfo
	{
		public int CeilingCount { get; set; }

		public List<TreasureChestItemLotteryRate> LotteryRateList { get; set; }

		public long TreasureChestItemId { get; set; }
	}
}
