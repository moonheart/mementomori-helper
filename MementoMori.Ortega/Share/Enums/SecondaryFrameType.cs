using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("第2フレーム種類")]
	public enum SecondaryFrameType
	{
		[Description("不明")]
		None,
		[Description("属性アイコン")]
		ElementIcon,
		[Description("効果時間表示")]
		EffectTime,
		[Description("レベル")]
		Level,
		[Description("キャラアイコン_中央")]
		CenteredCharacterIcon
	}
}
