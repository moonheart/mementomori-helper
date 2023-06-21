using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("カウントリセットタイプ")]
	public enum ShopCountResetType
	{
		[Description("日単位のカウントリセット")]
		Daily = 1,
		[Description("週単位のカウントリセット")]
		Weekly
	}
}
