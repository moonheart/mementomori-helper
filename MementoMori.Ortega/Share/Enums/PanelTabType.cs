using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("パネル図鑑のタブタイプ")]
	public enum PanelTabType : byte
	{
		[Description("イベント")]
		Event = 1,
		[Description("タイトル")]
		Title
	}
}
