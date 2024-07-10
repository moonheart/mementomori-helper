using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum GuildEventPolicyType
	{
		[Description("指定なし")]
		None,
		[Description("自由参加")]
		Freely,
		[Description("気軽に参加")]
		Leisurely,
		[Description("上位を狙う")]
		Seriously
	}
}
