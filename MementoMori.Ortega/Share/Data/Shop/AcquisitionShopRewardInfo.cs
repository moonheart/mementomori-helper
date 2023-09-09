using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class AcquisitionShopRewardInfo
	{
		public List<UserItem> BonusItemList { get; set; }

		public List<UserCharacterDtoInfo> CharacterList { get; set; }

        public List<UserItem> ItemList { get; set; }

        public ShopProductType ShopProductType { get; set; }
	}
}
