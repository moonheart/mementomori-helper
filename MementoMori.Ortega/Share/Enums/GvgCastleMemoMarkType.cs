using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum GvgCastleMemoMarkType
	{
		[Description("攻撃(強)")]
		StrongAttack = 1,
		[Description("攻撃(弱)")]
		Attack,
		[Description("防衛(強)")]
		StrongDefense,
		[Description("防衛(弱)")]
		Defense,
		[Description("汎用")]
		Common,
		[Description("禁止")]
		Forbidden
	}
}
