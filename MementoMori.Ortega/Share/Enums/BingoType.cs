using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ビンゴ種別")]
	public enum BingoType
	{
		[Description("不明")]
		None,
		[Description("上段")]
		UpperRow,
		[Description("中段")]
		CenterRow,
		[Description("下段")]
		LowerRow,
		[Description("左列")]
		LeftColumn,
		[Description("中列")]
		CenterColumn,
		[Description("右列")]
		RightColumn
	}
}
