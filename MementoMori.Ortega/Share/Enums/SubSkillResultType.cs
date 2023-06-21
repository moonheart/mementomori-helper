using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("サブスキルリザルトの種類")]
	public enum SubSkillResultType
	{
		[Description("基本")]
		Base,
		[Description("パッシブ")]
		Passive,
		[Description("臨時処理")]
		Temp
	}
}
