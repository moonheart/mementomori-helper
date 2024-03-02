using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("時空の洞窟 マス状態")]
	public enum DungeonBattleGridState
	{
		[Description("すべて完了し、次のマスに進むことができる")]
		Done,
		[Description("選択した")]
		Selected,
		[Description("報酬が未受け取り")]
		Reward,
		[Description("スキップ時のミステリーショップ")]
		SkipShop
	}
}
