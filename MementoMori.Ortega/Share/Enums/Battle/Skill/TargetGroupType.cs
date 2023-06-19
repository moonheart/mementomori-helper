using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("スキル対象グループタイプ")]
	public enum TargetGroupType
	{
		[Description("なし")]
		None,
		[Description("自分自身")]
		Self,
		[Description("全体")]
		All = 10,
		[Description("味方全体")]
		AllyAll,
		[Description("敵全体")]
		EnemyAll,
		[Description("自分以外の全体")]
		AllExceptSelf,
		[Description("自分以外の味方全体")]
		AllyAllExceptSelf,
		[Description("固定1")]
		Fix1 = 50,
		[Description("固定2")]
		Fix2
	}
}
