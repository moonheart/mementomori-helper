using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ガチャ追加キャラタイプ")]
	public enum GachaAddCharacterType
	{
		[Description("未指定")]
		None,
		[Description("復刻")]
		Reprint,
		[Description("新規")]
		New
	}
}
