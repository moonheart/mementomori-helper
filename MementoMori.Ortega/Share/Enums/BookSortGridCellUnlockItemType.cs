using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("書庫整理マス開放アイテムのタイプ")]
	public enum BookSortGridCellUnlockItemType
	{
		[Description("None")]
		None,
		[Description("1マス解放")]
		Square1X1,
		[Description("横2マス解放")]
		Square2X1,
		[Description("横4マス解放")]
		Square4X1,
		[Description("縦4マス解放")]
		Square1X4,
		[Description("正方形4マス解放")]
		Square2X2,
		[Description("十字9マス解放")]
		Cross,
		[Description("クロス9マス解放")]
		DiagonalCross,
		[Description("正方形9マス解放")]
		Square3X3
	}
}
