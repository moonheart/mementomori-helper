using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum BanChatType
	{
		[Description("不明")]
		None,
		[Description("全てのチャット")]
		All,
		[Description("ワールド、ワールドグループチャット")]
		WorldAndWorldGroup
	}
}
