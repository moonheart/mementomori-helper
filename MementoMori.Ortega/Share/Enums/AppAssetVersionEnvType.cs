using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum AppAssetVersionEnvType
	{
		[Description("不明")]
		None,
		[Description("sbx環境")]
		Sbx,
		[Description("stg環境")]
		Stg,
		[Description("prd環境")]
		Prd
	}
}
