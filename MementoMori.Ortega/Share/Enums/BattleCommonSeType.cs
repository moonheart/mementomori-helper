using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("バトルの基本SE")]
	public enum BattleCommonSeType
	{
		[Description("バトル開始")]
		Start,
		[Description("バトル終了 勝ち")]
		EndWin,
		[Description("バトル終了 負け")]
		EndLose,
		[Description("トドメ")]
		Todome,
		[Description("味方 力尽きる")]
		DeathAlly,
		[Description("敵 力尽きる")]
		DeathEnemy
	}
}
