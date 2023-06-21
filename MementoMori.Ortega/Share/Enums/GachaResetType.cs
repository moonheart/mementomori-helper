using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ガチャリセットタイプ")]
	public enum GachaResetType
	{
		[Description("リセット無し")]
		None,
		[Description("毎日4:00")]
		Daily,
		[Description("毎週月曜4:00")]
		Weekly
	}
}
