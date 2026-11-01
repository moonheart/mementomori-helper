using MementoMori.Ortega.Share.Data.Shop;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share.Data.Interface
{
	public interface IShopProductLimitPack
	{
		long BadgeImageId { get; }

		long BigImageId { get; }

		int BuyCountPeriod { get; }

		int BuyLimitCount { get; }

		int DiscountRate { get; }

		string EndDateTime { get; }

		long ImageId { get; }

		bool IsFree { get; }

		string NameKey { get; }

		string ProductId { get; }

		long ResetTime { get; }

		ShopBuyLimitType ShopBuyLimitType { get; }

		int ShopProductPrice { get; }

		List<ShopItem> UserItemList { get; }

		ShopProductType ShopProductType { get; }

		long MBId { get; }

		long OpenVipLevel { get; }

		long RequiredPointGrade { get; }

		bool IsAvailableTab()
		{
			return true;
		}

		bool IsInTime(DateTime localDateTime)
		{
			// 元の IL は復元できなかったため、EndDateTime との比較で再実装
			return string.IsNullOrEmpty(EndDateTime) || localDateTime <= EndDateTime.ToDateTime();
		}
	}
}
