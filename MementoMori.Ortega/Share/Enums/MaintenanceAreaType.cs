using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ターゲット領域タイプ")]
	public enum MaintenanceAreaType
	{
		[Description("全て")]
		All,
		[Description("ゲームサーバー単位")]
		GameServer,
		[Description("ワールド単位")]
		World
	}
}
