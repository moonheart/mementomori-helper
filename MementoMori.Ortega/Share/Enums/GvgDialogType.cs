using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("GvGのダイアログタイプ")]
	public enum GvgDialogType
	{
		[Description("不明")]
		None,
		[Description("バトルダイアログ")]
		Battle,
		[Description("編成ダイアログ")]
		Deploy
	}
}
