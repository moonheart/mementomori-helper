using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum BattleFieldCharacterGroupType
	{
		[Description("攻撃側。バトル画面の左側")]
		Attacker,
		[Description("防衛側。バトル画面の右側")]
		Receiver
	}
}
