using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("エフェクトグループアイコンタイプ")]
	public enum EffectGroupIconType
	{
		[Description("なし")]
		None,
		[Description("キャラクター")]
		Character,
		[Description("加護")]
		Blessing,
		[Description("敵")]
		Enemy,
		[Description("専属武器")]
		ExclusiveWeapon
	}
}
