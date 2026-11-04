using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("メンテナンスプラットフォームリスト")]
	public enum MaintenancePlatformType
	{
		[Description("全て")]
		All,
		[Description("アップストア")]
		AppStore,
		[Description("グーグルプレイストア")]
		GooglePlayStore,
		[Description("Windows")]
		Win64 = 4,
		[Description("Dmmストア")]
		DmmStore,
		[Description("Steam")]
		Steam,
		[Description("Apk")]
		Apk
	}
}
