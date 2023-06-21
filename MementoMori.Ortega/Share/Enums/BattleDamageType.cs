using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("バトルダメージタイプ")]
	[Obsolete("TODO 2019-11-29 takeda 廃止予定")]
	public enum BattleDamageType
	{
		[Description("物理")]
		Physical,
		[Description("魔法")]
		Magic
	}
}
