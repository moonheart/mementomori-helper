using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("スキル強化アイテム")]
	[Flags]
	public enum SkillEnhancementItemFlag
	{
		[Description("技能書1")]
		SkillEnhancementItem1 = 1,
		[Description("技能書2")]
		SkillEnhancementItem2 = 2,
		[Description("技能書3")]
		SkillEnhancementItem3 = 4
	}
}
