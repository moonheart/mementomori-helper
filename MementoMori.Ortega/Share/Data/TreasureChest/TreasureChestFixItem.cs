using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TreasureChest
{
	[MessagePackObject(true)]
	[Description("宝箱固定アイテム")]
	public class TreasureChestFixItem
	{
		[Nest(true, 2)]
		[Description("アイテム")]
		public UserItem FixItem { get; set; }

		[Description("神器タイプ")]
		public SacredTreasureType SacredTreasureType { get; set; }
	}
}
