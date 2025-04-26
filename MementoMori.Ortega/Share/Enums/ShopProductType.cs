using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum ShopProductType
	{
		[Description("買い切り一般商品")]
		Default,
		[Description("買い切りダイヤ商品")]
		Currency,
		[Description("VIP情報")]
		VipInfo,
		[Description("魔女の贈り物")]
		GrowthPack = 4,
		[Description("盟約特権")]
		ContractPrivilege,
		[Description("月間ブースト")]
		MonthlyBoost,
		[Description("課金機能付きミッション")]
		CurrencyMission,
		[Description("初課金ボーナス")]
		FirstChargeBonus,
		[Description("達成パック")]
		AchievementPack,
		[Description("チャージ特典")]
		ChargeBonus,
		[Description("ゲリラパック")]
		GuerrillaPack,
		[Description("ミッションパス")]
		MissionPass,
		[Description("全検索")]
		AllSearch = 99
	}
}
