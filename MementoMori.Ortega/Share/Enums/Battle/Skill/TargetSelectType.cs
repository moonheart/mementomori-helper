using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("スキルターゲット選択タイプ")]
	public enum TargetSelectType
	{
		[Description("全体")]
		All = 1,
		[Description("ランダム")]
		Random,
		[Description("順番的に選択")]
		Select = 10,
		[Description("一番目のターゲットとあのターゲットに隣接しているターゲット")]
		FirstSelectAndNeighbor = 49,
		[Description("ランダムターゲットとあのターゲットに隣接しているターゲット")]
		RandomAndNeighbor,
		[Description("使用者の隣接か、使用者の正面の隣接")]
		Neighbor,
		[Description("正面")]
		Front,
		[Description("正面と隣接の中でランダム")]
		RandomFrontAndNeighbor,
		[Description("正面を確定し、さらに両隣からランダムで選ぶ")]
		FrontAndRandomNeighbor
	}
}
