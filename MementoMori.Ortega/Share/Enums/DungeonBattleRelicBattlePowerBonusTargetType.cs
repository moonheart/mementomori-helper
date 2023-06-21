using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("時空の洞窟\u3000戦闘力ボーナス対象")]
	public enum DungeonBattleRelicBattlePowerBonusTargetType
	{
		[Description("すべて")]
		All,
		[Description("愁（しゅう）")]
		ElementBlue,
		[Description("業（ごう）")]
		ElementRed,
		[Description("心（しん）")]
		ElementGreen,
		[Description("渇（かつ）")]
		ElementYellow,
		[Description("ウォリアー")]
		Warrior,
		[Description("スナイパー")]
		Sniper,
		[Description("ソーサラー")]
		Sorcerer
	}
}
