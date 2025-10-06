using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("PatternSettingタイプ")]
	public enum PatternSettingType
	{
		[Description("なし")]
		None,
		[Description("マイページバナーの外部Webサイトリンク")]
		MyPageBannerOuterWebSite,
		[Description("ミッションの表示優先度")]
		MissionDisplayOrder,
		[Description("ミッションのガイド表示優先度")]
		MissionGuideDisplayOrder,
		[Description("ゲリラパックの報酬内容")]
		[Obsolete]
		GuerrillaPackRewardItem,
		[Description("ダイナミックリンクによるワールド誘導")]
		InviteWorldByDynamicLink,
		[Description("初課金ボーナスのダイヤ購入ボタンの遷移先")]
		FirstChargeBonusButton,
		[Description("一週間限定パックの購入期限プッシュ通知")]
		OneWeekLimitedPackPushNotification
	}
}
