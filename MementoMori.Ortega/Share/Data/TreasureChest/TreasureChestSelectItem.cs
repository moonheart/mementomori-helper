using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TreasureChest
{
	[MessagePackObject(true)]
	[Description("宝箱選択アイテム")]
	public class TreasureChestSelectItem
	{
		[Nest(true, 1)]
		[Description("アイテム")]
		public UserItem SelectItem { get; set; }

		[Description("神器タイプ")]
		public SacredTreasureType SacredTreasureType { get; set; }
	}
}
