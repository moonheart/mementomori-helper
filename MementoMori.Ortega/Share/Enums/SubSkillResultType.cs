using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("サブスキルリザルトの種類")]
	public enum SubSkillResultType
	{
		
		[Description("なし")]
		None,
		[Description("ダメージ")]
		Damage,
		[Description("効果")]
		Effect,
		[Description("パッシブ")]
		Passive,
		[Description("臨時処理")]
		Temp
	}
}
