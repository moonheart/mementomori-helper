using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	[Description("ショップバッジ情報")]
	public class ShopBadgeInfo
	{
		public bool CanGetAchievementPack { get; set; }

		public bool CanGetChargeBonus { get; set; }

		public bool CanGetCurrencyMission { get; set; }

		public bool CanGetDefault { get; set; }

		public bool CanGetFirstChargeBonus { get; set; }

		public bool CanGetGrowthPack { get; set; }

		public bool CanGetMonthlyBoost { get; set; }

		public bool CanGetVipDaily { get; set; }

		public ShopBadgeInfo()
		{
			this.CanGetAchievementPack = false;
		}

		public bool IsDisplayBadge()
		{
			return this.CanGetAchievementPack || this.CanGetChargeBonus || this.CanGetCurrencyMission || this.CanGetDefault || this.CanGetFirstChargeBonus || this.CanGetGrowthPack || this.CanGetMonthlyBoost || this.CanGetVipDaily;
		}
	}
}
