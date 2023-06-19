using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("高速周回実行タイプ")]
	public enum QuestQuickExecuteType
	{
		[Description("仮想通貨で実行")]
		Currency,
		[Description("特典で実行")]
		Privilege
	}
}
