using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TreasureChest
{
	[MessagePackObject(true)]
	[Description("宝箱の抽選アイテム")]
	public class TreasureChestLotteryItem : ILotteryWeight
	{
		[Description("天井の対象となるアイテムか")]
		public bool IsCeilingTarget { get; set; }

		[Nest(true, 2)]
		public UserItem Item { get; set; }

		[Description("神器タイプ")]
		public SacredTreasureType SacredTreasureType { get; set; }

		[Description("抽選重み")]
		public int LotteryWeight { get; set; }
	}
}
