using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Obsolete("α2でスキルの構造が変わったため、削除予定です！")]
	[Description("スキルタイプ")]
	public enum SkillType
	{
		[Description("攻撃")]
		Attack,
		[Description("回復")]
		Recovery
	}
}
