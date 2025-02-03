using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("フレンド画面の取得データ")]
	public enum FriendInfoType
	{
		[Description("なし")]
		None,
		[Description("フレンド")]
		Friend,
		[Description("承認待ち")]
		ApprovalPending,
		[Description("申請中")]
		Applying,
		[Description("ブロック")]
		Block,
		[Description("おすすめ検索")]
		Recommend,
		[Description("模擬戦")]
		FriendBattle
	}
}
