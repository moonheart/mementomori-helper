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
		[Description("Dmmストア")]
		DmmStore = 5
	}
}
