using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ゲリラクエスト報酬用キャラクターのレアリティポイント合計")]
	public enum RewardRarityPointGroupType
	{
		[Description("なし")]
		None,
		[Description("3~5")]
		ThreeToFive,
		[Description("6")]
		Six,
		[Description("7~8")]
		SevenToEight,
		[Description("9")]
		Nine,
		[Description("10~11")]
		TenToEleven,
		[Description("12")]
		Twelve,
		[Description("13~14")]
		ThirteenToFourteen,
		[Description("15")]
		Fifteen
	}
}
