using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductInfo
	{
		[Description("表示順(昇順)")]
		public int DisplayOrder { get; set; }

		[Description("商品MBのId")]
		public long MbId { get; set; }

		[Description("達成パック用データ")]
		public ShopProductAchievementPack ShopProductAchievementPack { get; set; }

        [Description("チャージ特典用データ")]
		public ShopProductChargeBonus ShopProductChargeBonus { get; set; }

        [Description("盟約特権用データ")]
		public ShopProductContractPrivilege ShopProductContractPrivilege { get; set; }

        [Description("買い切りダイヤ商品用データ")]
		public ShopProductCurrency ShopProductCurrency { get; set; }

        [Description("課金機能付きミッション用データ")]
		public ShopProductCurrencyMission ShopProductCurrencyMission { get; set; }

        [Description("買い切り一般商品用データ")]
		public ShopProductDefault ShopProductDefault { get; set; }

        [Description("初課金ボーナス用データ")]
		public ShopProductFirstChargeBonus ShopProductFirstChargeBonus { get; set; }

        [Description("魔女の贈り物用データ")]
		public ShopProductGrowthPack ShopProductGrowthPack { get; set; }

        [Description("ゲリラパック用データ")]
		public ShopProductGuerrillaPack ShopProductGuerrillaPack { get; set; }

        [Description("月間ブースト用データ")]
		public ShopProductMonthlyBoost ShopProductMonthlyBoost { get; set; }

        [Description("商品種別タイプ")]
		public ShopProductType ShopProductType { get; set; }
	}
}
