using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("無窮の塔報酬タイプ")]
	public enum TowerBattleRewardsType
	{
		None,
		[Description("初回")]
		First,
		[Description("確定")]
		Confirmed,
		[Description("確率")]
		Lottery
	}
}
