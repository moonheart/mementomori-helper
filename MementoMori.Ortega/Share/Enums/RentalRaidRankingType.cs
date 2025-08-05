using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum RentalRaidRankingType
	{
		[Description("前半レイドステージの編成制限ランキング")]
		FirstHalfLimitFormationRanking,
		[Description("前半レイドステージの最大ダメージランキング")]
		FirstHalfMaxDamageRanking,
		[Description("後半レイドステージの編成制限ランキング")]
		SecondHalfLimitFormationRanking,
		[Description("後半レイドステージの最大ダメージランキング")]
		SecondHalfMaxDamageRanking
	}
}
