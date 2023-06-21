using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("運命ガチャタイプ")]
	public enum GachaDestinyType
	{
		[Description("なし")]
		None,
		[Description("愁、業、心、渇属性")]
		BlueAndRedAndGreenAndYellow,
		[Description("こちらの承認")]
		LightAndDark
	}
}
