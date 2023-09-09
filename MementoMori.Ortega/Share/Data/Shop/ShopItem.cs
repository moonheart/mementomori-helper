using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	[Description("ショップアイテム")]
	public class ShopItem : IUserCharacterItem
	{
		public CharacterRarityFlags RarityFlags { get; set; }

		[Nest(true, 1)]
		public UserItem Item { get; set; }
    }
}
