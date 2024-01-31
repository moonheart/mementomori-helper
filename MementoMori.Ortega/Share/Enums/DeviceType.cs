using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("端末&プラットフォーム種別")]
	public enum DeviceType
	{
		[Description("iOS")]
		iOS = 1,
		[Description("Android")]
		Android,
		[Description("Unity")]
		UnityEditor,
		[Description("Windows")]
		Win64,
		[Description("DmmGames")]
		DmmGames,
		[Description("Steam")]
		Steam,
        [Description("Apk")]
        Apk
	}
}
