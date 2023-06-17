using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ミッション状態種別")]
	public enum MissionStatusType
	{
		[Description("未解放")]
		Locked,
		[Description("進行中")]
		Progress,
		[Description("未受取")]
		NotReceived,
		[Description("獲得済")]
		Received
	}
}
