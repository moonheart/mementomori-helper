using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Obsolete("α2でスキルの構造が変わったため、削除予定です！")]
	[Description("対象キャラクター")]
	public enum TargetType
	{
		[Description("単体")]
		OnlyOne,
		[Description("全体")]
		All
	}
}
