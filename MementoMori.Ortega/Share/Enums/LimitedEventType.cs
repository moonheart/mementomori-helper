using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("期間限定イベントの種類")]
	public enum LimitedEventType
	{
		[Description("不明")]
		None,
		[Description("属性の塔全開放")]
		ElementTowerAllRelease,
		[Description("シリアルコード入力")]
		SerialCode,
		[Description("動画再生コメント投稿")]
		PlayVideoComment,
		[Description("動画再生再生回数表示")]
		PlayVideoTotalCount,
		[Description("祈りの泉報酬2倍")]
		BountyQuestRewardBonus,
		[Description("動画配信モードON時、対象プレイヤーのみ動画再生可能")]
		PlayVideoIgnoreLiveMode,
		[Description("年齢確認法の制御を解除")]
		DisableAgeAssurance,
		[Description("通知強制削除")]
		NotificationForceCancel = 10000,
		[Description("GooglePlayのレシート消費をクライアントで行う")]
		EnableGooglePlayReceiptConsumeByClient,
		[Description("Deeplinkのエラー判定 PurchaseStateType.FetchProductWaitチェック有効")]
		CheckDeeplinkPurchaseStateFetchProductWait,
		[Description("Webストアのリリース管理")]
		WebstoreRelease = 20000,
		[Description("アセットのキャッシュクリア無効")]
		DisableAssetCacheClear = 30000
	}
}
