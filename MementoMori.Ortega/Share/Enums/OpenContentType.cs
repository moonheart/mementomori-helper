using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("コンテンツ開放タイプ")]
	public enum OpenContentType
	{
		[Description("ランクアップ")]
		RankUp,
		[Description("クエストクリア")]
		QuestClear,
		[Description("パーティレベル")]
		PartyLevel,
		[Description("UR以上専用武器レアリティ数")]
		ExclusiveEquipmentURRarityCount
	}
}
