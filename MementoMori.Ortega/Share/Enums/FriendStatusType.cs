using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("フレンド状態種別")]
	public enum FriendStatusType
	{
		[Description("初期値")]
		None,
		[Description("フレンドでない")]
		Stranger,
		[Description("フレンド")]
		Friend,
		[Description("フレンド申請中")]
		Applying,
		[Description("承認待ち")]
		Receive
	}
}
