using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum PlayerGuildBattlePolicyType
	{
		[Description("指定なし")]
		None,
		[Description("リアル優先")]
		Freely,
		[Description("そこそこ貢献")]
		Leisurely,
		[Description("上位を狙う")]
		Seriously
	}
}
