using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum RentalRaidStageOpenEffectType
	{
		None,
		[Description("前半レイド(ステージ2)")]
		FirstHalfBoss,
		[Description("後半ノーマル(ステージ3)")]
		SecondHalfNormal,
		[Description("後半レイド(ステージ4)")]
		SecondHalfBoss
	}
}
