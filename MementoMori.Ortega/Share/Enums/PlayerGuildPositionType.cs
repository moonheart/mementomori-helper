using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum PlayerGuildPositionType
	{
		None,
		[Description("マスター")]
		Leader,
		[Description("ベテラン")]
		Veteran,
		[Description("メンバー")]
		Member,
		[Description("サブマスター")]
		SubLeader,
		[Description("指揮官")]
		Commander
	}
}
