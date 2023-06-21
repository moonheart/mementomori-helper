using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("システムチャット種別")]
	public enum SystemChatType
	{
		None,
		[Description("LocalGvg(ワールド)")]
		LocalGvgWorld,
		[Description("LocalGvg(ギルド)")]
		LocalGvgGuild,
		[Description("GlobalGvg(ワールド)")]
		GlobalGvgWorld,
		[Description("GlobalGvg(ギルド)")]
		GlobalGvgGuild,
		[Description("LegendLeague")]
		LegendLeague,
		[Description("BattleLeague")]
		BattleLeague,
		[Description("Guild")]
		Guild,
		[Description("ChangePlayerName")]
		ChangePlayerName
	}
}
