using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Obsolete("α2でスキルの構造が変わったため、削除予定です！")]
	[Description("次のコマンドにつながる条件")]
	public enum CommandChainType
	{
		[Description("つながらない")]
		None,
		[Description("常につながる")]
		Always,
		[Description("誰かを倒すとつながる")]
		KillAny = 10
	}
}
