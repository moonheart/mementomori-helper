using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("期間限定イベントの種類")]
	public enum LimitedEventType
	{
        [Description("不明")]
        None,
        [Description("属性の塔全開放")]
        ElementTowerAllRelease,
        [Description("シリアルコード入力")]
        SerialCode,
        [Description("古い課金システムの使用")]
        ApplyOldPurchaseSystem = 100,
        [Description("ギルドレイドoffset設定")]
        GuildRaidCharacterPositionByMB,
        [Description("GvGでキャラクターのキャッシュが存在しない場合に例外を投げる")]
        ThrowExceptionInGvgWhenCharacterCacheNotExists
	}
}
