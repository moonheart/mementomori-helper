using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Present
{
	[MessagePackObject(true)]
	public class PresentItem : IUserCharacterItem, IDeepCopy<PresentItem>
	{
		[Nest(true, 2)]
		[Description("アイテム")]
		[PropertyOrder(1)]
		public UserItem Item
		{
			get;
			set;
		}

		[PropertyOrder(2)]
		[Description("キャラクターレアリティ")]
		public CharacterRarityFlags RarityFlags
		{
			get;
			set;
		}

		public static PresentItem Create(IUserItem userItem, CharacterRarityFlags rarityFlags)
		{
			PresentItem presentItem = new PresentItem();
			UserItem userItem2 = userItem.ToUserItem();
			presentItem.Item = userItem2;
			presentItem.RarityFlags = rarityFlags;
			return presentItem;
		}

		public PresentItem DeepCopy()
		{
			PresentItem presentItem = new PresentItem();
			UserItem userItem = this.Item.DeepCopy();
			presentItem.Item = userItem;
			CharacterRarityFlags characterRarityFlags = this.RarityFlags;
			presentItem.RarityFlags = characterRarityFlags;
			return presentItem;
		}

		public PresentItem()
		{
		}
	}
}
