using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ユーザー設定データ")]
	public enum PlayerSettingsType
	{
		[Description("不明")]
		None,
		[Description("レアリティNのキャラ自動販売")]
		AutoSellRarityNCharacter
	}
}
