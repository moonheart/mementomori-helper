using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("フレンド画面の並び替え種別")]
	public enum PlayerOrderType
	{
		[Description("最終アクセス順")]
		LastAccess,
		[Description("レベル順")]
		PlayerLevel,
		[Description("戦闘力順")]
		BattlePower,
		[Description("職業順")]
		Job,
		[Description("追加日時順")]
		Added
	}
}
