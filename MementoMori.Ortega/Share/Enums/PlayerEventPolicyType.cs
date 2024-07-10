using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum PlayerEventPolicyType
	{
		[Description("指定なし")]
		None,
		[Description("マイペース")]
		Freely,
		[Description("毎日参加")]
		Daily,
		[Description("上位を狙う")]
		Seriously
	}
}
